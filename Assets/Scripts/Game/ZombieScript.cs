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
    private float baseSpeed = 7f; // Speed of the agent

    [SerializeField]
    private float followRadius = 10f; // Radius within which the zombie will follow the player

    [SerializeField]
    private float wanderRadius = 20f; // Radius within which the zombie will move randomly

    private Vector3 wanderDestination; // Destination for random movement
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = baseSpeed; // Set the speed of the agent to the base speed
        // Initialyse the destination of the agent
        SetRandomWanderDestination();
    }

    void Update()
    {
        // Verify if the playerTransform is null
        if (playerTransform == null)
        {
            // Find the player and assign it to the playerTransform variable
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            return;
        }

        // Calculate the distance between the zombie and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= followRadius)
        {
            // Direction in which the zombie should look
            Vector3 lookDirection = playerTransform.position - transform.position;
            lookDirection.y = 0; // Keep the zombie upright
            
            // Rotate the zombie towards the player
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * 360f);

            // Update the destination of the agent to the player's position
            agent.SetDestination(playerTransform.position);
        }
        else
        {
            // Verify if the agent has reached its destination
            if (!agent.hasPath || agent.remainingDistance < 1f)
            {
                SetRandomWanderDestination();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // Verify if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player has been caught");
            // Unfocuse the cursor
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("LooseScene");
        }
    }

    // Increase the speed of the agent
    public void IncreaseSpeed(float speedIncreaseAmount)
    {
        agent.speed += speedIncreaseAmount; // Increase the speed of the agent
    }

    // Set the destination of the agent to a random position within the wanderRadius
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