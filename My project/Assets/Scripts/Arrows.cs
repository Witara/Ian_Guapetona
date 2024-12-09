using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 5f; // Speed of upward movement
    public Vector2 screenBounds; // Bounds for deletion

    void Update()
    {
        // Move the object upwards
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

        // Check if the object is out of bounds and destroy it
        if (transform.position.y > screenBounds.y)
        {
            Destroy(gameObject);
        }
    }
}
