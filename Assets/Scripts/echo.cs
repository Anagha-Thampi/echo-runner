using UnityEngine;
using System.Collections.Generic;

public class EchoFollower : MonoBehaviour
{
    public Transform player;          // Assign player in Inspector
    public float followDelay = 0.3f;  // Delay in seconds
    public float moveSpeed = 15f;     // Smooth movement speed
    public float recordInterval = 0.02f;

    private Queue<Vector3> positions = new Queue<Vector3>();
    private float recordTimer = 0f;

    void Update()
    {
        if (player == null) return;

        // Record player positions
        recordTimer += Time.deltaTime;
        if (recordTimer >= recordInterval)
        {
            positions.Enqueue(player.position);
            recordTimer = 0f;
        }

        // Start moving after enough positions are recorded
        int framesToDelay = Mathf.RoundToInt(followDelay / recordInterval);
        if (positions.Count > framesToDelay)
        {
            Vector3 targetPos = positions.Dequeue();
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}