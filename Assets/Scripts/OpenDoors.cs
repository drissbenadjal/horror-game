using System.Collections;
using UnityEngine;
using TMPro;

public class OpenDoors : MonoBehaviour
{
    private GameObject DoorD_V1_TOP;
    private GameObject DoorD_Left;
    private GameObject DoorD_Right;
    private GameObject Player;
    private GameObject SoundOpeningDoor;
    private GameObject MessageText;

    public bool isDoorOpen = false;
    public float rotationSpeed = 100f; // Vitesse de rotation des portes en degrés par seconde

    void Start()
    {
        DoorD_V1_TOP = GameObject.Find("DoorD_V1_TOP");
        DoorD_Left = GameObject.Find("DoorD_Left_TOP");
        DoorD_Right = GameObject.Find("DoorD_Right_TOP");
        SoundOpeningDoor = GameObject.Find("Sound Opening Door");
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");
    }

    void Update()
    {
        if (IsPlayerInFrontOfDoor())
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = isDoorOpen ? "Press E to close the door" : "Press E to open the door";
            MessageText.SetActive(true);
        }
        else
        {
            MessageText.SetActive(false);
            MessageText.GetComponent<TextMeshProUGUI>().text = "";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerInFrontOfDoor())
            {
                if (isDoorOpen)
                {
                    StartCoroutine(CloseDoor());
                }
                else
                {
                    StartCoroutine(OpenDoor());
                }
            }
            else
            {
                Debug.Log("Vous n'êtes pas devant la porte");
            }
        }
    }

    bool IsPlayerInFrontOfDoor()
    {
        float proximityThreshold = 1.3f;
        Vector3 doorPosition = DoorD_V1_TOP.transform.position;
        Vector3 playerPosition = Player.transform.position;

        return Mathf.Abs(playerPosition.x - doorPosition.x) < proximityThreshold &&
               Mathf.Abs(playerPosition.z - doorPosition.z) < proximityThreshold;
    }

    IEnumerator OpenDoor()
    {
        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            DoorD_Left.transform.Rotate(Vector3.up, -rotationStep);
            DoorD_Right.transform.Rotate(Vector3.up, -rotationStep);
            angle += rotationStep;
            yield return null;
        }

        isDoorOpen = true;
    }

    IEnumerator CloseDoor()
    {
        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            DoorD_Left.transform.Rotate(Vector3.up, rotationStep);
            DoorD_Right.transform.Rotate(Vector3.up, rotationStep);
            angle += rotationStep;
            yield return null;
        }

        isDoorOpen = false;
    }
}