using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public GameObject spikePrefab;
    public GameObject wallPrefab; // New wall prefab
    public Transform[] spawnPoints; // Array of spawn points
    public Transform player; // Assign the player GameObject or its transform here
    
    // Public variables for spike properties
    public float spikeDestroyTime = 5f; // Lifetime of the spawned spikes
    public float spikeCooldown = 3f; // Cooldown time between spike spawns
    public float spikeSpeed = 5f; // Speed of the spikes
    public float fadeInTime = 0.5f; // Time it takes for the spikes to fade in

    // Public variables for burst attack
    public float burstDuration = 2f; // Duration of burst attack
    public float burstBulletSpeedIncrease = 5f; // Increase in bullet speed during burst attack
    public float burstCooldownDecrease = 1f; // Decrease in cooldown during burst attack

    private float timer = 0f;
    private bool isBurstActive = false; // Flag to track burst attack

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= (isBurstActive ? spikeCooldown / 2 : spikeCooldown))
        {
            StartCoroutine(SpawnSpikeCoroutine());
            timer = 0f;
        }

        // Check for burst attack activation
        if (Input.GetKeyDown(KeyCode.Alpha2) && !isBurstActive)
        {
            StartCoroutine(ActivateBurst());
        }

        // Check for wall attack activation
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(SpawnWallCoroutine());
        }
    }

    IEnumerator SpawnSpikeCoroutine()
    {
        // Only spawn spikes at the first spawn point
        Transform spawnPoint = spawnPoints[2];

        // Spawn the spike and point it towards the player
        GameObject spikeObject = Instantiate(spikePrefab, spawnPoint.position, Quaternion.identity);
        Vector3 directionToPlayer = (player.position - spikeObject.transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        spikeObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Apply velocity to the spike and all its children
        Rigidbody2D[] rigidbodies = spikeObject.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in rigidbodies)
        {
            // Set the velocity, ignoring the Z component
            rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * (isBurstActive ? spikeSpeed + burstBulletSpeedIncrease : spikeSpeed);
        }

        // Destroy the spike after spikeDestroyTime seconds
        Destroy(spikeObject, spikeDestroyTime);

        yield return null;
    }

    IEnumerator ActivateBurst()
    {
        isBurstActive = true;

        // Decrease the cooldown during burst attack
        spikeCooldown -= burstCooldownDecrease;

        yield return new WaitForSeconds(burstDuration);

        isBurstActive = false;

        // Reset the cooldown to its original value
        spikeCooldown += burstCooldownDecrease;
    }

    IEnumerator SpawnWallCoroutine()
    {
        // Only spawn wall at the third spawn point
        Transform spawnPoint = spawnPoints[2];

        // Spawn the wall and point it towards the player
        GameObject wallObject = Instantiate(wallPrefab, spawnPoint.position, Quaternion.identity);
        Vector3 directionToPlayer = (player.position - wallObject.transform.position).normalized;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        wallObject.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Apply velocity to the wall and its children
        Rigidbody2D[] wallRigidbodies = wallObject.GetComponentsInChildren<Rigidbody2D>();
        foreach (Rigidbody2D rb in wallRigidbodies)
        {
            if (rb != null) // Check if Rigidbody2D is not null
            {
                rb.velocity = new Vector2(directionToPlayer.x, directionToPlayer.y) * (isBurstActive ? spikeSpeed + burstBulletSpeedIncrease - 2f : spikeSpeed - 2f);
            }
        }

        // Destroy the wall and its children after a delay
        yield return new WaitForSeconds(spikeDestroyTime);
        Destroy(wallObject);
    }
}
