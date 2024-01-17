using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    public new string name;

    public float damage;
    public float maxDistance;

    public int currentAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public float inspectTime;
    public bool reloading;
    public bool inspecting;

    private void OnEnable()
    {
        InitializeDefaults();
    }

    private void InitializeDefaults()
    {
        reloading = false;
        inspecting = false;
        currentAmmo = magSize;
    }

    // Method to perform null checks and initialize defaults if necessary
    public void PerformNullChecks()
    {
        if (name == null)
        {
            Debug.LogWarning("GunData name is null. Setting to default.");
            name = "Gun";
        }

        if (reloading && currentAmmo < 0)
        {
            Debug.LogWarning("GunData has invalid currentAmmo value. Resetting to default.");
            currentAmmo = 15;
        }

        if (magSize <= 0)
        {
            Debug.LogWarning("GunData has invalid magSize value. Resetting to default.");
            magSize = 15;
        }

        if (fireRate <= 0)
        {
            Debug.LogWarning("GunData has invalid fireRate value. Resetting to default.");
            fireRate = 1.0f;
        }

        if (reloadTime <= 0)
        {
            Debug.LogWarning("GunData has invalid reloadTime value. Resetting to default.");
            reloadTime = 1.0f;
        }
       
        if (inspectTime <= 0)
        {
            Debug.LogWarning("GunData has invalid reloadTime value. Resetting to default.");
            inspectTime = 1.0f;
        }
    }
}
