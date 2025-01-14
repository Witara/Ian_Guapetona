using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn; // The prefab to spawn
    private float spawnDelay; // Delay between spawns
    public Vector2 screenBounds; // Define screen bounds for object deletion
    public static List<GameObject> spawnedArrows = new List<GameObject>();

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
        }
    }

    void SpawnObject()
    {
        // Instantiate the object
        GameObject newObject = Instantiate(objectToSpawn);
        spawnedArrows.Add(newObject);
        // Randomly choose one of the four directions
        int directionIndex = Random.Range(0, 4);
        string directionText = "Up"; // Default direction text
        float xPosition = 0f; // Default X position
        Vector3 rotation = Vector3.zero; // Default rotation

        switch (directionIndex)
        {
            case 0: // Up
                directionText = "Up";
                rotation = new Vector3(0f, 0f, -90f);
                xPosition = -3f;
                break;
            case 1: // Down
                directionText = "Up";
                rotation = new Vector3(0f, 0f, -270f);
                xPosition = -4.2f;
                break;
            case 2: // Left
                directionText = "Up";
                rotation = new Vector3(0f, 0f, -360f);
                xPosition = -5.2f;
                break;
            case 3: // Right
                directionText = "Up";
                rotation = new Vector3(0f, 0f, -180f);
                xPosition = -1.9f;
                break;
        }

        // Set the rotation and position
        newObject.transform.eulerAngles = rotation;
        newObject.transform.position = new Vector3(xPosition, -50, -3);

        // Assign the direction to the MovingObject script
        MovingObject movingObjectScript = newObject.GetComponent<MovingObject>();
        if (movingObjectScript != null)
        {
            movingObjectScript.screenBounds = screenBounds;
            movingObjectScript.arrowDirection = directionText; // Pass direction info
        }
    }
}
