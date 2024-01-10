using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public GunData gundata;
    public TextMeshProUGUI ammoDisplay;
    public TextMeshProUGUI weaponname;
    public string slash = "/";
    public string reloading = "reloading..";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        weaponname.text = gundata.name;
        ammoDisplay.text = gundata.currentAmmo.ToString() + slash + gundata.magSize;
        if (gundata.reloading == true)
        {
            ammoDisplay.text = reloading;
        }
    }
    
}
