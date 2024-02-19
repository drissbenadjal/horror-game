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
        // Ajouter le temps écoulé depuis la dernière mise à jour
        timeElapsed += Time.deltaTime;

        // Vérifier si le temps écoulé est supérieur à 4 secondes
        if (timeElapsed >= 45f)
        {
            // Réinitialiser le temps écoulé
            timeElapsed = 0f;

            // Effectuer la rotation de 90 degrés sur l'axe Y
            RotateZombie();
        }
    }

    void RotateZombie()
    {
        // Obtenir la rotation actuelle du zombie
        Vector3 currentRotation = zombie.transform.eulerAngles;

        // Ajouter 90 degrés à la rotation sur l'axe Y
        currentRotation.y += 180f;

        // Appliquer la nouvelle rotation au zombie
        zombie.transform.eulerAngles = currentRotation;
    }
}