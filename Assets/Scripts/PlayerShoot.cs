using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;

    public static Action reloadInput;

    public KeyCode reloadkey;


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadkey))
        {
            reloadInput?.Invoke();
        }

    }
}
