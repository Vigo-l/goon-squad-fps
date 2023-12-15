using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dooranimation : MonoBehaviour
{
    public Animator opendooranimator;
    public bool doorvisible;
    // Start is called before the first frame update
    void Start()
    {

        doorvisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorvisible == true)
        {
            opendooranimator.SetBool("Startdoor", true);
        }
    }
}
