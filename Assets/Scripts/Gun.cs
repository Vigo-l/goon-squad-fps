using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Transform Muzzle;
    public AudioSource Gunshot;

    Animator weaponAnimator;

    float timeSinceLastShot;
    public void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        weaponAnimator = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<Animator>();
    }
    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        weaponAnimator.SetBool("reloading", true);

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
        weaponAnimator.SetBool("reloading", false);


    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        RaycastHit hit;
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
               Gunshot.Play(); 
                if (Physics.Raycast(Muzzle.position, Muzzle.transform.forward, out  hit, gunData.maxDistance))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.TakeDamage(gunData.damage);
                    }
                    
                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

  
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
    }

    private void OnGunShot()
    {

    }
}
