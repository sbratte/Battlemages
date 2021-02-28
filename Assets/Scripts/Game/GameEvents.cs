using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEvents : MonoBehaviour
{
    public static int killCount;

    // Update is called once per frame
    void Update()
    {
        if (HealthSystem.Instance.hitPoints < 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("GameOverMenu");
        }

        if (killCount == 12)
        {
            Cursor.lockState = CursorLockMode.Locked;
            SceneManager.LoadScene("GameWonMenu");
        }
    }
}
