using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool Paused = false;
    public GameObject CanvasUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            if (Paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }


    public void Resume() {
        CanvasUI.SetActive(false);
        Time.timeScale = 1f;
        Paused = false;
    }

    public void Pause() {
        CanvasUI.SetActive(true);
        Time.timeScale = 0f;
        Paused = true;
    }
    
}
