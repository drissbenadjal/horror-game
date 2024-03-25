using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    private InputAction openAction;
    public InputActionAsset actions;

    void Start()
    {
        // Récupère la référence à l'action "open" depuis votre ActionAsset
        openAction = actions.FindActionMap("Player").FindAction("PauseMenu");
        openAction.Enable();
        openAction.performed += ctx => togglePauseMenu();
    }
    
    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape) || (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
    //     {
    //         if (GameIsPaused)
    //         {
    //             Cursor.lockState = CursorLockMode.Locked;
    //             Resume();
    //         }
    //         else
    //         {
    //             Pause();
    //         }
    //     }
    // }

    private void togglePauseMenu()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    // Resume the game
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
    }

    // Pause the game
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("MenuScene");
    }

    // Quit the game
    public void QuitGame()
    {
        Application.Quit();
    }
}