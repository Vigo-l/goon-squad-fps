using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    public TextMeshProUGUI Healthdisplay;
    public string slash = "/";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Healthdisplay.text = PlayerMovement.currentHealth.ToString() + slash + PlayerMovement.maxHealth;
    }
}
