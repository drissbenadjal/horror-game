using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class OpenSimpleDoor : MonoBehaviour
{
    public InputActionAsset actions;
    public GameObject Door_Cadre;
    public GameObject Door_Left;
    public GameObject Screamer;
    public GameObject Screamer_Audio;
    private GameObject Player;
    private GameObject SoundOpeningDoor;
    private GameObject SoundLockDoor;
    private GameObject MessageText;
    private bool animationIsRunning = false;

    public bool isDoorOpen = false;
    public bool isLockDoor = false;
    public float rotationSpeed = 110f;
    private bool clearText = false;
    public bool ScreamerIsPlay = false;

    private InputAction openAction;

    void Start()
    {
        SoundOpeningDoor = GameObject.Find("Opening Doors Sound");
        SoundLockDoor = GameObject.Find("Locked Doors Sound");
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");


        // Récupère la référence à l'action "open" depuis votre ActionAsset
        actions.FindActionMap("Player").FindAction("Action").Enable();
        openAction = actions.FindActionMap("Player").FindAction("Action");
        openAction.performed += ctx => OpenCloseDoor();
    }

    void Update()
    {
        if (IsPlayerInFrontOfDoor())
        {
            MessageText.GetComponent<TextMeshProUGUI>().text = isDoorOpen ? TextManager.PressEToCloseDoor : TextManager.PressEToOpenDoor;;
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
        openAction.Disable();
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

    void OpenCloseDoor()
    {
        if (animationIsRunning)
        {
            return;
        }
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
                    animationIsRunning = true;
                    StartCoroutine(CloseDoor());
                }
                else
                {
                    animationIsRunning = true;
                    StartCoroutine(OpenDoor());
                }
            }
        }
    }

    IEnumerator OpenDoor()
    {
        if (Screamer != null && ScreamerIsPlay == false)
        {
            Screamer.SetActive(true);
            if (Screamer_Audio != null)
            {
                Screamer_Audio.GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(1);
                Screamer.SetActive(false);
                Screamer_Audio.GetComponent<AudioSource>().Stop();
            }
            else
            {
                yield return new WaitForSeconds(1);
                Screamer.SetActive(false);
            }
            ScreamerIsPlay = true;
        }

        SoundOpeningDoor.GetComponent<AudioSource>().Play();
        float angle = 0f;

        while (angle < 90f)
        {
            float rotationStep = rotationSpeed * Time.deltaTime;
            Door_Left.transform.Rotate(Vector3.up, -rotationStep);
            angle += rotationStep;
            yield return null;
        }
        animationIsRunning = false;

        isDoorOpen = true;
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
        animationIsRunning = false;

        isDoorOpen = false;
    }
}