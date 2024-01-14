using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonScript : MonoBehaviour
{
    public Camera Player;
    public string sceneName;
    public string sceneName2;
    public string sceneName3;
    public string sceneName4;

    public AudioSource piano;
    public AudioSource sound;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Player.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.CompareTag("Button"))
                {
                    loadingScene(sceneName);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.Confined;
                }
                if (hit.collider != null && hit.collider.CompareTag("Button2"))
                {
                    loadingScene(sceneName2);
                }
                if (hit.collider != null && hit.collider.CompareTag("Button3"))
                {
                    loadingScene(sceneName3);
                }
                if (hit.collider != null && hit.collider.CompareTag("Button4"))
                {
                    loadingScene(sceneName4);
                }
                if (hit.collider != null && hit.collider.CompareTag("Piano"))
                {
                    sound.Stop();
                    piano.Play();
                }
            }
        }
    }

    public void loadingScene(string sceneName)
    {
        Debug.Log(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
