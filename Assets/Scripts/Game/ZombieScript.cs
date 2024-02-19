using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;

    [SerializeField]
    private float baseSpeed = 7f; // Vitesse de déplacement de base de l'agent

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed; // Définir la vitesse de l'agent sur la vitesse de base
    }

    void Update()
    {
        // Vérifier si le playerTransform est défini
        if (playerTransform == null)
        {
            // Vous pouvez récupérer le joueur de la scène ou d'une autre manière
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        // Direction vers laquelle le zombie doit se tourner
        Vector3 lookDirection = playerTransform.position - transform.position;
        lookDirection.y = 0; // Ne pas inclure la rotation verticale
        
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 130, 0);

        // Rotation progressive du zombie vers le joueur
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 360f);

        // Mettre à jour la destination de l'agent
        agent.SetDestination(playerTransform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'agent entre en collision avec le joueur
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has been caught");
        }
    }

    // Fonction pour augmenter la vitesse de l'agent
    public void IncreaseSpeed(float speedIncreaseAmount)
    {
        agent.speed += speedIncreaseAmount; // Augmenter la vitesse de l'agent de la quantité spécifiée
    }
}