using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public Transform player;              // Reference to the player
    public float chaseRange = 10f;        // Distance at which the enemy will start chasing
    public float rotationSpeed = 5f;      // Speed at which the enemy rotates towards the player

    private NavMeshAgent agent;           // NavMeshAgent for movement
    private int currentPatrolIndex;       // Index for the current patrol point
    private bool isChasing = false;       // Whether the enemy is chasing the player
    private Transform[] patrolPoints;     // Array to hold patrol points

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;

        GameObject[] patrolObjects = GameObject.FindGameObjectsWithTag("PatrolPoint");

        if (patrolObjects.Length == 0) {
            Debug.LogError("No patrol points found in the scene. Please tag the patrol points with 'PatrolPoint'.");
            return;
        }

        patrolPoints = new Transform[patrolObjects.Length];
        for (int i = 0; i < patrolObjects.Length; i++) {
            patrolPoints[i] = patrolObjects[i].transform;
        }

        currentPatrolIndex = 0;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void Update() {
        if (Vector3.Distance(player.position, transform.position) <= chaseRange) {
            StartChasingPlayer();
        }
        else if (isChasing) {
            StopChasingPlayer();
        }
        // else if (QuizManage.examActive)
        // {
        //     ReactToExam();
        // }

        if (!isChasing) {
            Patrol();
        }
    }

    private void Patrol() {
        if (agent.remainingDistance <= agent.stoppingDistance) {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    private void StartChasingPlayer() {
        isChasing = true;
        agent.SetDestination(player.position);
    }

    private void StopChasingPlayer() {
        isChasing = false;
        StartCoroutine(TurnBeforePatrolling());
    }

    private void FacePlayer() {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // Rotate the enemy by 90 degrees around the Y-axis
        Quaternion additionalRotation = Quaternion.Euler(0, 90, 0);

        // Combine the two rotations
        targetRotation *= additionalRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private System.Collections.IEnumerator TurnBeforePatrolling() {
        // Stop the agent while turning
        agent.isStopped = true;

        // Perform a random rotation
        float randomAngle = Random.Range(150f, 210f);
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + randomAngle, 0);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f) {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        // Resume patrolling
        agent.isStopped = false;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    void LateUpdate() {
        if (isChasing) {
            FacePlayer();
        }
    }

    // Method that reacts when an exam is attempted
    public void ReactToExam() {
        // React by starting to chase the player (or any other behavior you want)
        isChasing = true;
        agent.SetDestination(player.position);  // Make the enemy chase the player after an exam attempt
        Debug.Log("Enemy is reacting to the exam!");
    }
}