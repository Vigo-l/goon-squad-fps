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
    public GunData gunData;
    public Transform cam;
    public Dooranimation Dooranimation;
    public AudioSource Gunshot;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float maxBulletDistance = 50f;
    public int enemyCount = 0;
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
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        PlayerShoot.inspectInput += StartInspect;
        weaponAnimator = GameObject.FindGameObjectWithTag("WeaponHolder")?.GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
    }

    public void StartReload()
    {
        if (!gunData.reloading)
        {
            StopCoroutine("Reload");
            StartCoroutine(Reload());
        }
    }
    public void StartInspect()
    {
        if (!gunData.inspecting)
        {
            StartCoroutine(Inspect());
        }
    }

    private void OnDestroy()
    {
        if (IsInvoking("Reload"))
        {
            StopCoroutine("Reload");
        }
    }


    private IEnumerator Reload()
    {
        if (gunData != null)
        {
            gunData.reloading = true;
            weaponAnimator?.SetBool("reloading", true);

            yield return new WaitForSeconds(gunData.reloadTime);

            gunData.currentAmmo = gunData.magSize;

            gunData.reloading = false;
            weaponAnimator?.SetBool("reloading", false);
        }
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
                OnGunShot();
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
    }

    void OnGunShot()
    {
        if (bulletSpawnPoint != null && bulletPrefab != null)
        {
            // Instantiate a new bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Set the bullet's initial velocity
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.velocity = bulletSpawnPoint.forward * bulletSpeed;
            }

            // Destroy the bullet if it's too far away
            DestroyIfTooFar(bullet);
        }

        void DestroyIfTooFar(GameObject bullet)
        {
            if (bullet != null)
            {
                float distance = Vector3.Distance(bullet.transform.position, transform.position);
                if (distance > maxBulletDistance)
                {
                    // Make sure to check if the bullet and its components are null before destroying
                    if (bullet.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(bullet.GetComponent<Rigidbody>());
                    }

                    // Check if the bullet object is null before destroying
                    Destroy(bullet);
                }
            }
        }
    }
}
