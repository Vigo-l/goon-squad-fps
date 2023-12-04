using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Transform cam;
    public Dooranimation Dooranimation;
    public AudioSource Gunshot;
    public AudioSource Emptyammo;
    public int enemyCount = 0;
    public int requiredEnemyCount = 5;
    public GameObject door;
    public Image emptygun;
    Animator weaponAnimator;
    string currentScene;

    float timeSinceLastShot;
    public void Start()
    {
        door.SetActive(false);
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        weaponAnimator = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<Animator>();
        string currentScene = SceneManager.GetActiveScene().name;



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
                if (Physics.Raycast(cam.position, cam.transform.forward, out  hit, gunData.maxDistance))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    Debug.Log(hit.transform.name);
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

  
    public void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (enemyCount >= requiredEnemyCount)
        {
            door.SetActive(true);
            Dooranimation.doorvisible = true;
        }
        if (currentScene == "level2")
        {
            requiredEnemyCount = 6;
        }
        if (currentScene == "Level desert(2)")
        {
            requiredEnemyCount = 20;
        }
    }
    public void OnGunShot()
    {

    }
}
