using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class OpenDoors : MonoBehaviour
{
    public GameObject DoorD_V1_TOP;
    public GameObject DoorD_Left;
    public GameObject DoorD_Right;
    private GameObject Player;
    private GameObject SoundOpeningDoor;
    private GameObject MessageText;
    private bool animationIsRunning = false;

    public bool isDoorOpen = false;
    public float rotationSpeed = 110f;
    private bool clearText = false;

    private InputAction openAction;
    public InputActionAsset actions;

    void Start()
    {
        SoundOpeningDoor = GameObject.Find("Opening Doors Sound");
        MessageText = GameObject.Find("MessageText");
        Player = GameObject.Find("PlayerCapsule");

        // Récupère la référence à l'action "open" depuis votre ActionAsset
        openAction = actions.FindActionMap("Player").FindAction("Action");
        openAction.Enable();
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

    void OpenCloseDoor()
    {
        if (animationIsRunning)
        {
            return;
        }
        if (IsPlayerInFrontOfDoor())
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

    bool IsPlayerInFrontOfDoor()
    {
        float proximityThreshold = 1.3f;
        Vector3 doorPosition = DoorD_V1_TOP.transform.position;
        Vector3 playerPosition = Player.transform.position;

        return Mathf.Abs(playerPosition.x - doorPosition.x) < proximityThreshold &&
               Mathf.Abs(playerPosition.z - doorPosition.z) < proximityThreshold &&
                Mathf.Abs(playerPosition.y - doorPosition.y) < proximityThreshold;
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
            DoorD_Left.transform.Rotate(Vector3.up, rotationStep);
            DoorD_Right.transform.Rotate(Vector3.up, rotationStep);
            angle += rotationStep;
            yield return null;
        }
        animationIsRunning = false;
        isDoorOpen = false;
    }
}
