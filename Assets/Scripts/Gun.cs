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
    public AudioSource Emptyammo;
    public int enemyCount = 0;
    public int requiredEnemyCount = 5;
    public int orbCount = 0;
    public int requiredOrbCount = 5;
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
        weaponAnimator = GameObject.FindGameObjectWithTag("WeaponHolder")?.GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;
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

    private bool CanShoot() => gunData != null && !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        RaycastHit hit;
        if (gunData != null && gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                Gunshot?.Play();
                if (Physics.Raycast(cam?.position ?? Vector3.zero, cam?.transform?.forward ?? Vector3.forward, out hit, gunData.maxDistance))
                {
                    Enemy enemy = hit.transform?.GetComponent<Enemy>();
                    Debug.Log(hit.transform?.name);
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
