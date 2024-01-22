using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    //faire un truc
    //faire en sorte que ce qui y a dedans bouge DoorD_Left et DoorD_Right s'ouvre (rotation) quand on appuie sur E
    public bool isDoorOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        DoorD_Left = GameObject.Find("DoorD_Left_TOP");
        DoorD_Right = GameObject.Find("DoorD_Right_TOP");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoorD_V1_TOP = GameObject.Find("DoorD_V1_TOP");
            Player = GameObject.Find("Player");
            //SI LE JOUEUR EST DEVANT LA PORTE
            Debug.Log("Player: " + Player.transform.position.x + " " + Player.transform.position.z);
            Debug.Log("DoorD_V1_TOP: " + DoorD_V1_TOP.transform.position.x + " " + DoorD_V1_TOP.transform.position.z);
            
            if (Player.transform.position.x < DoorD_V1_TOP.transform.position.x + 2 && Player.transform.position.x > DoorD_V1_TOP.transform.position.x - 2 && Player.transform.position.z < DoorD_V1_TOP.transform.position.z + 2 && Player.transform.position.z > DoorD_V1_TOP.transform.position.z - 2)
            {
                //SI LA PORTE EST OUVERTE
                if (isDoorOpen)
                {
                    DoorD_Left.transform.Rotate(0, 90, 0);
                    DoorD_Right.transform.Rotate(0, 90, 0);
                    isDoorOpen = false;
                }
                else
                {
                    DoorD_Left.transform.Rotate(0, -90, 0);
                    DoorD_Right.transform.Rotate(0, -90, 0);
                    isDoorOpen = true;
                }
            } else
            {
                Debug.Log("Vous n'êtes pas devant la porte");
            }
        }
    }

    private GameObject DoorD_V1_TOP;
    private GameObject DoorD_Left;
    private GameObject DoorD_Right;

    private GameObject Player;
}