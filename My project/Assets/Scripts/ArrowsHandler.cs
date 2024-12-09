using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    private float spawnDelay; // Delay between spawns
    public Vector2 screenBounds; // Define screen bounds for object deletion

    void Start()
    {
        spawnDelay = Random.Range(0.5f, 1f);

        // Calculate screen bounds if not set
        if (screenBounds == Vector2.zero)
        {
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z * -1));
            }
        }

        // Start spawning objects with a delay
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(spawnDelay);

            // Spawn a new object if possible
            if (objectToSpawn != null)
            {
                SpawnObject();
            }
            else
            {
                Debug.LogError("objectToSpawn is not assigned or has been destroyed!");
            }
        }
    }

    void SpawnObject()
    {
        // Instantiate the object
        GameObject newObject = Instantiate(objectToSpawn);

        // Randomly choose one of the four directions
        int directionIndex = Random.Range(0, 4);
        string directionText = "Up"; // Default direction text
        float xPosition = 0f; // Default X position
        Vector3 rotation = Vector3.zero; // Default rotation

        switch (directionIndex)
        {
            case 0: // Up
                directionText = "Up";
                rotation = new Vector3(0f, 0f, -90f); // Set rotation for Up
                xPosition = 2f; // Consistent X position for Up
                break;
            case 1: // Down
                directionText = "Down";
                rotation = new Vector3(0f, 0f, -270f); // Set rotation for Down
                xPosition = 0f; // Consistent X position for Down
                break;
            case 2: // Left
                directionText = "Left";
                rotation = new Vector3(0f, 0f, -360f); // Set rotation for Left
                xPosition = -2f; // Consistent X position for Left
                break;
            case 3: // Right
                directionText = "Right";
                rotation = new Vector3(0f, 0f, -180f); // Set rotation for Right
                xPosition = 4f; // Consistent X position for Right
                break;
        }

        // Set the rotation and position
        newObject.transform.eulerAngles = rotation; // Apply manual rotation
        newObject.transform.position = new Vector3(xPosition, -50, 0);

        // Assign screen bounds to the object for deletion logic
        MovingObject movingObjectScript = newObject.GetComponent<MovingObject>();
        if (movingObjectScript != null)
        {
            movingObjectScript.screenBounds = screenBounds;
        }

        // Log the chosen direction and rotation
        Debug.Log($"Spawned Object Facing: {directionText} at X: {xPosition} with rotation {rotation}");
    }
}
