using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class ZombieScript : MonoBehaviour
{
    public Transform playerTransform;
    NavMeshAgent agent;

    [SerializeField]
    private float baseSpeed = 7f; // Vitesse de déplacement de base de l'agent

    [SerializeField]
    private float followRadius = 10f; // Rayon dans lequel le zombie commencera à suivre le joueur

    [SerializeField]
    private float wanderRadius = 20f; // Rayon de déplacement aléatoire lorsque le joueur n'est pas à proximité

    private Vector3 wanderDestination; // Destination de déplacement aléatoire

    [SerializeField]
    private GameObject soundProximity; // Objet pour le son de proximité
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed; // Définir la vitesse de l'agent sur la vitesse de base

        // Initialiser la première destination de déplacement aléatoire
        SetRandomWanderDestination();
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

        // Calculer la distance entre le zombie et le joueur
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followRadius)
        {
            //jouer le son
            soundProximity.GetComponent<AudioSource>().Play();
            // Direction vers laquelle le zombie doit se tourner
            Vector3 lookDirection = playerTransform.position - transform.position;
            lookDirection.y = 0; // Ne pas inclure la rotation verticale
            
            // Rotation progressive du zombie vers le joueur
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 360f);

            // Mettre à jour la destination de l'agent pour suivre le joueur
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            soundProximity.GetComponent<AudioSource>().Stop();
            // soundProximity.GetComponent<AudioSource>().time = 0;
            // Si le joueur n'est pas à proximité, déplacer le zombie aléatoirement
            if (!agent.hasPath || agent.remainingDistance < 1f)
            {
                SetRandomWanderDestination();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // Vérifier si l'agent entre en collision avec le joueur
        // Debug.Log("Collision detected");
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player has been caught");
            SceneManager.LoadScene("LooseScene");
        }
    }

    // Fonction pour augmenter la vitesse de l'agent
    public void IncreaseSpeed(float speedIncreaseAmount)
    {
        agent.speed += speedIncreaseAmount; // Augmenter la vitesse de l'agent de la quantité spécifiée
    }

    // Définir une destination de déplacement aléatoire dans le rayon de déplacement aléatoire
    private void SetRandomWanderDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, 1);
        wanderDestination = hit.position;
        agent.SetDestination(wanderDestination);
    }
}