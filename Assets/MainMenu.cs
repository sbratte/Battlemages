using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayMedieval()
    {
        SceneManager.LoadScene("MedievalScene");
    }

    public void PlayForest()
    {
        SceneManager.LoadScene("ForestScene");
    }

    public void Back()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}
