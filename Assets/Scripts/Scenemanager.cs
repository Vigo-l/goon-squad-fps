using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public void Level1()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level desert(2)");
    }
    public void Level3()
    {
        SceneManager.LoadScene("CityLevel");
    }
    public void Home()
    {
        SceneManager.LoadScene("Home");
    }
}
