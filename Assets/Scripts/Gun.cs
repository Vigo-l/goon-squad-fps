using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Gun : MonoBehaviour
{
    [Header("Objects")]
    public GunData gunData;
    public Transform cam;

    [Header("Audio")]
    public AudioSource Gunshot;

    public Dooranimation Dooranimation;

    public GameObject muzzleflash;


    public int enemyCount = 0;
    public static Action shootInput;

    public static Action reloadInput;
    public static Action inspectInput;
    public float Muzzletime;
    public KeyCode reloadkey;
    public KeyCode inspectkey;
    public int requiredEnemyCount = 5;
    public int orbCount = 0;
    public int requiredOrbCount = 5;
    public GameObject door;
    private Ray ray;
    public Image emptygun;
    Animator weaponAnimator;
    string currentScene;

    float timeSinceLastShot;

    public void Awake()
    {
        door.SetActive(false);
        weaponAnimator = GameObject.FindGameObjectWithTag("WeaponHolder")?.GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
        muzzleflash.SetActive(false);
    }

    public void StartReload()
    {
        StartCoroutine(Reload());
    }
    public void StartInspect()
    {

        StartCoroutine(Inspect());

    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        weaponAnimator?.SetBool("reloading", true);

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
        weaponAnimator?.SetBool("reloading", false);
    }
    private IEnumerator Inspect()
    {
        if (gunData != null)
        {
            gunData.inspecting = true;
            weaponAnimator?.SetBool("inspecting", true);

            yield return new WaitForSeconds(gunData.inspectTime);

            gunData.inspecting = false;
            weaponAnimator?.SetBool("inspecting", false);
        }
    }

    private bool CanShoot() => gunData != null && !gunData.reloading && !gunData.inspecting && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        RaycastHit hit;
        if (cam != null && gunData != null && gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                OnGunShot();
                if (Physics.Raycast(cam.position, cam.transform.forward, out hit, gunData.maxDistance))
                {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    Debug.Log("YO");
                    if (enemy != null)
                    {
                        enemy.TakeDamage(gunData.damage);
                        
                    }
                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
            }
        }
    }


    public void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        if (enemyCount >= requiredEnemyCount && door != null && Dooranimation != null)
        {
            door.SetActive(true);
            Dooranimation.doorvisible = true;
        }
        if (orbCount >= requiredOrbCount && door != null && Dooranimation != null)
        {
            door.SetActive(true);
            Dooranimation.doorvisible = true;
        }
        if (Input.GetMouseButton(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(reloadkey))
        {
            StartReload();
        }
        if (Input.GetKeyDown(inspectkey))
        {
            StartInspect();
        }
    }
    public void OnGunShot()
    {
        Gunshot.Play();
        StartCoroutine(muzzleFlash());
    }

    private IEnumerator muzzleFlash()
    {
        muzzleflash.SetActive(true);
        yield return new WaitForSeconds(Muzzletime);
        muzzleflash.SetActive(false);

    }

    }
