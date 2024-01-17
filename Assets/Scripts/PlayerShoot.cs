using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;

    public static Action reloadInput;
    public static Action inspectInput;

    public KeyCode reloadkey;
    public KeyCode inspectkey;


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
        if (Input.GetKeyDown(inspectkey))
        {
            inspectInput?.Invoke();
        }

    }
}
