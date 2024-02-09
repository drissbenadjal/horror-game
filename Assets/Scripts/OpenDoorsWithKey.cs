using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class OpenDoorsWithKey : MonoBehaviour
{
    [SerializeField] private GameObject Cadre;
    public GameObject DoorLeft;
    public GameObject DoorRight;

    public string KeyName;

    public GameObject SoundOpeningDoor;

    private GameObject Player;
    private GameObject MessageText;

    public KeysManager m_KeysManager;

    public bool isDoorOpen = false;

    public float rotationSpeed = 110f;

    private bool animationIsRunning = false;

    private bool clearText = false;

    void Start()
    {
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");
    }

    void Update()
    {
        if (IsPlayerInFrontOfDoor())
        {
            if (IsPlayerHasKey())
            {
                MessageText.GetComponent<TextMeshProUGUI>().text = "Press E to open the door";
            }
            else
            {
                MessageText.GetComponent<TextMeshProUGUI>().text = "You don't have the key to open the door";
            }
            clearText = true;
        }
        else if (clearText)
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = "";
            clearText = false;
        }

        if (Input.GetKeyDown(KeyCode.E) || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame))
        {
            if (animationIsRunning)
            {
                return;
            }
            if (IsPlayerInFrontOfDoor())
            {
                if (IsPlayerHasKey())
                {
                    if (isDoorOpen)
                    {
                        animationIsRunning = true;
                        StartCoroutine(CloseDoor());
                    }
                    else
                    {
                        animationIsRunning = true;
                        StartCoroutine(OpenDoor());
                    }
                }
                else
                {
                    // Debug.Log("Vous n'avez pas la clé");
                }
            }
        }
    }

    private bool IsPlayerInFrontOfDoor()
    {
        if (Vector3.Distance(Player.transform.position, Cadre.transform.position) < 2f)
        {
            return true;
        }
        return false;
    }

    private bool IsPlayerHasKey()
    {
        foreach (string key in m_KeysManager.PlayerKeys)
        {
            // Debug.Log(key);
            if (key == KeyName)
            {
                // Debug.Log("Vous avez la clé");
                return true;
            }
        }
        return false;
    }

    IEnumerator OpenDoor()
    {
        animationIsRunning = true;
        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            DoorLeft.transform.Rotate(Vector3.up, -rotationStep);
            if (DoorRight != null)
            {
                DoorRight.transform.Rotate(Vector3.up, -rotationStep);
            }
            angle += rotationStep;
            yield return null;
        }
        animationIsRunning = false;
        isDoorOpen = true;
    }

    IEnumerator CloseDoor()
    {
        animationIsRunning = true;
        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            DoorLeft.transform.Rotate(Vector3.up, rotationStep);
            if (DoorRight != null)
            {
                DoorRight.transform.Rotate(Vector3.up, rotationStep);
            }
            angle += rotationStep;
            yield return null;
        }
        animationIsRunning = false;
        isDoorOpen = false;
    }
}
