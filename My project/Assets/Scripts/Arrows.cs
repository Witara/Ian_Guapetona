using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed = 5f; // Speed of upward movement
    public Vector2 screenBounds; // Bounds for deletion
    public string arrowDirection; // Direction of this arrow

    private AmongUsHandler amongUsHandler;
    private ShrekHandler shrekHandler;
    private SkibidiHandler skibidiHandler;
    private SmurfHandler smurfHandler;
    private RickHandler rickHandler;

    
    void Start()
    {
        // Find the object with the AmongUsHandler script
        amongUsHandler = Object.FindFirstObjectByType<AmongUsHandler>(); // Updated method
        shrekHandler = Object.FindFirstObjectByType<ShrekHandler>(); // Updated method
        skibidiHandler = Object.FindFirstObjectByType<SkibidiHandler>(); // Updated method
        smurfHandler = Object.FindFirstObjectByType<SmurfHandler>(); // Updated method
        rickHandler = Object.FindFirstObjectByType<RickHandler>(); // Updated method

    }

    void Update()
    {
        // Move the object upwards
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);

        // Check if the object is out of bounds and destroy it
        if (transform.position.y > screenBounds.y)
        {
            // Trigger animation on another object before destroying
            if (amongUsHandler != null)
            {
                amongUsHandler.SetAnimationDirection(arrowDirection);
                shrekHandler.SetAnimationDirection(arrowDirection);
                skibidiHandler.SetAnimationDirection(arrowDirection);
                smurfHandler.SetAnimationDirection(arrowDirection);
                rickHandler.SetAnimationDirection(arrowDirection);
            }
            Destroy(gameObject);
        }
    }
}
