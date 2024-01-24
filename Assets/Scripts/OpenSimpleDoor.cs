using System.Collections;
using UnityEngine;
using TMPro;

public class OpenSimpleDoor : MonoBehaviour
{
    public GameObject Door_Cadre;
    public GameObject Door_Left;
    public GameObject Screamer;
    public GameObject Screamer_Audio;
    private GameObject Player;
    private GameObject SoundOpeningDoor;
    private GameObject SoundLockDoor;
    private GameObject MessageText;

    public bool isDoorOpen = false;
    public bool isLockDoor = false;
    public float rotationSpeed = 110f;

    public bool ScreamerIsPlay = false;

    void Start()
    {
        // Door_Cadre = GameObject.Find("Door_Cadre_Gatehouse");
        // Door_Left = GameObject.Find("Door_Gatehouse");
        SoundOpeningDoor = GameObject.Find("Sound Opening Door");
        SoundLockDoor = GameObject.Find("Sound Lock Door");
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
                if (isLockDoor)
                {
                    SoundLockDoor.GetComponent<AudioSource>().Play();
                }
                else
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
            }
            else
            {
                // Debug.Log("Vous n'êtes pas devant la porte");
            }
        }
    }

    bool IsPlayerInFrontOfDoor()
    {
        float proximityThreshold = 1.3f;
        Vector3 doorPosition = Door_Cadre.transform.position;
        Vector3 playerPosition = Player.transform.position;

        return Mathf.Abs(playerPosition.x - doorPosition.x) < proximityThreshold &&
               Mathf.Abs(playerPosition.z - doorPosition.z) < proximityThreshold &&
                Mathf.Abs(playerPosition.y - doorPosition.y) < proximityThreshold;
    }

    IEnumerator OpenDoor()
    {
        if (Screamer != null && ScreamerIsPlay == false)
        {
            Screamer.SetActive(true);
            if (Screamer_Audio != null)
            {
                Screamer_Audio.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(4);
                Screamer.SetActive(false);
                Screamer_Audio.GetComponent<AudioSource>().Stop();
            }
            else
            {
                yield return new WaitForSeconds(4);
                Screamer.SetActive(false);
            }
            ScreamerIsPlay = true;
        }

        isDoorOpen = true;

        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            Door_Left.transform.Rotate(Vector3.up, -rotationStep);
            angle += rotationStep;
            yield return null;
        }
    }

    IEnumerator CloseDoor()
    {
        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            Door_Left.transform.Rotate(Vector3.up, rotationStep);
            angle += rotationStep;
            yield return null;
        }

        isDoorOpen = false;
    }
}