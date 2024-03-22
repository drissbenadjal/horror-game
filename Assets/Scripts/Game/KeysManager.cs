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

    public InputActionAsset actions;

    private bool clearText = false;

    void Start()
    {
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");
        SoundKeyPickUp = GameObject.Find("PickUp Key Sound");

        // Récupère la référence à l'action "pickup" depuis votre ActionAsset
        actions.FindActionMap("Player").FindAction("Action").Enable();
        InputAction pickUpAction = actions.FindActionMap("Player").FindAction("Action");
        pickUpAction.performed += ctx => PickUpKey();
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
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").FindAction("Action").Disable();
    }

    private bool IsPlayerInFrontOfKey()
    {
        foreach (GameObject key in Keys)
        {
            if (Vector3.Distance(Player.transform.position, key.transform.position) < 2f)
            {
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
                }
                else
                {
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