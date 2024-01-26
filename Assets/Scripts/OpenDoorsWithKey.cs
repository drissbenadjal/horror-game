using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenDoorsWithKey : MonoBehaviour
{
    public GameObject Cadre;
    public GameObject DoorLeft;
    public GameObject DoorRight;

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
            if (IsPlayerHasKey()) {
            MessageText.GetComponent<TextMeshProUGUI>().text = "Press E to open the door";
            } else {
            MessageText.GetComponent<TextMeshProUGUI>().text = "You don't have the key to open the door";
            }
            clearText = true;
        } else if (clearText)
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = "";
            clearText = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (animationIsRunning)
            {
                return;
            }
            if (IsPlayerInFrontOfDoor())
            {
                if (IsPlayerHasKey())
                {
                    OpenDoor();
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
            Debug.Log(key);
            if (key == "Key1")
            {
                Debug.Log("Vous avez la clé");
                return true;
            }
        }
        return false;
    }


    IEnumerator OpenDoor()
    {
        Debug.Log("OpenDoor");
        return null;
    }
}
