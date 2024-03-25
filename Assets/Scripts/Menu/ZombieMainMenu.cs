using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMainMenu : MonoBehaviour
{
    public GameObject zombie;
    public float rotationSpeed = 90f; // Vitesse de rotation en degrés par seconde
    private float timeElapsed = 0f;

    void Update()
    {
        // Add the time elapsed since the last frame
        timeElapsed += Time.deltaTime;

        // Verify if 45 seconds have passed
        if (timeElapsed >= 45f)
        {
            // Reset the time elapsed
            timeElapsed = 0f;

            // Rotate the zombie
            RotateZombie();
        }
    }

    void RotateZombie()
    {
        // Take the current rotation of the zombie
        Vector3 currentRotation = zombie.transform.eulerAngles;

        // Add 180 degrees to the current rotation
        currentRotation.y += 180f;

        // Apply the new rotation to the zombie
        zombie.transform.eulerAngles = currentRotation;
    }
}