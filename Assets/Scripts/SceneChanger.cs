using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonScript : MonoBehaviour
{

    private void OnMouseUpAsButton()
    {
        home();
    }
    public void home()
    {
        SceneManager.LoadScene("Home");
    }
}