using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class KeysManager : MonoBehaviour
{
    public GameObject[] Keys;
    public List<string> PlayerKeys = new List<string>();

    private GameObject Player;
    private GameObject MessageText;

    private GameObject SoundKeyPickUp;

    private bool animationIsRunning = false;
    private bool clearText = false;

    void Start()
    {
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");
        SoundKeyPickUp = GameObject.Find("Sound Key PickUp");
    }

    void Update()
    {
        if (IsPlayerInFrontOfKey())
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = "Press E to pick up the key";
            clearText = true;
        }
        else if (clearText)
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = "";
            clearText = false;
        }

        if (Input.GetKeyDown(KeyCode.E) || (Gamepad.current != null && Gamepad.current.aButton.wasPressedThisFrame))
        {
            if (IsPlayerInFrontOfKey())
            {
                PickUpKey();
            }
        }
    }

    private bool IsPlayerInFrontOfKey()
    {
        foreach (GameObject key in Keys)
        {
            if (Vector3.Distance(Player.transform.position, key.transform.position) < 2f)
            {
                //si il est dans la liste des clés du joueur on ne l'affiche pas
                foreach (string playerKey in PlayerKeys)
                {
                    if (playerKey == key.name)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        return false;
    }

    private void PickUpKey()
    {
        foreach (GameObject key in Keys)
        {
            if (Vector3.Distance(Player.transform.position, key.transform.position) < 2f)
            {
                SoundKeyPickUp.GetComponent<AudioSource>().Play();
                key.SetActive(false);
                if (PlayerKeys.Count == 0)
                {
                    PlayerKeys.Add(key.name);
                    break;
                } else {
                    bool keyAlreadyInList = false;
                    foreach (string playerKey in PlayerKeys)
                    {
                        if (playerKey == key.name)
                        {
                            keyAlreadyInList = true;
                            break;
                        }
                    }
                    if (!keyAlreadyInList)
                    {
                        PlayerKeys.Add(key.name);
                        break;
                    }
                }
            }
        }
    }
}
