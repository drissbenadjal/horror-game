using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject settingsMenu;
    private GameObject mainMenu;

    public static bool isSettings = false;

    // public StarterAssets.FirstPersonController RotationSpeed;

    void Start()
    {
        mainMenu = GameObject.Find("MenuScene");

        // firstPersonController = GetComponent<StarterAssets.FirstPersonController>();

        // // Set the RotationSpeed of FirstPersonController
        // if (firstPersonController != null)
        // {
        //     firstPersonController.RotationSpeed = 1.0f;
        // }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isSettings)
            {
                if (settingsMenu.activeSelf)
                {
                    isSettings = false;
                    settingsMenu.SetActive(false);
                    mainMenu.SetActive(true);
                }
                else
                {
                    isSettings = true;
                    settingsMenu.SetActive(true);
                    mainMenu.SetActive(false);
                }
            }
        }
    }

    public void PlayGame()
    {
        // Load the next scene in the build index
        SceneManager.LoadScene("GameScene");
        //bloquer le curseur
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Settings()
    {
        if (settingsMenu.activeSelf)
        {
            isSettings = false;
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
        else
        {
            isSettings = true;
            settingsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    // Quit the game
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}