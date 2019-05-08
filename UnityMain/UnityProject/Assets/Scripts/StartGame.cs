using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void NewGame() {
        SceneManager.LoadScene("Level 1");
    }

    public void QuitGame() {
        Application.Quit();
    }

}
