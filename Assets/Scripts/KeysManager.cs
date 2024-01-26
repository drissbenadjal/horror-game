using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeysManager : MonoBehaviour
{
    public GameObject[] Keys;
    public List<string> PlayerKeys = new List<string>();

    private GameObject Player;
    private GameObject MessageText;
    private bool clearText = false;

    void Start()
    {
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");
    }

    void Update()
    {
        if (IsPlayerInFrontOfKey())
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = "Press E to pick up the key";
            clearText = true;
        } else if (clearText)
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = "";
            clearText = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
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
                key.SetActive(false);
                if (PlayerKeys.Count == 0)
                {
                    PlayerKeys.Add(key.name);
                    break;
                }
                for (int i = 0; i < PlayerKeys.Count; i++)
                {
                    if (PlayerKeys[i] == "")
                    {
                        PlayerKeys[i] = key.name;
                        break;
                    }
                }
                break;
            }
        }
    }
}
