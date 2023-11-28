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
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(Muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log("hiy0");
                    Gunshot.Play();
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
