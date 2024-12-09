using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockChildrenPosition : MonoBehaviour
{
    private List<Vector3> initialChildPositions = new List<Vector3>();

    void Start()
    {
        // Store the initial local positions of all child objects
        foreach (Transform child in transform)
        {
            initialChildPositions.Add(child.localPosition);
        }
    }

    void LateUpdate()
    {
        // Reset the local position of each child to its original position
        int index = 0;
        foreach (Transform child in transform)
        {
            child.localPosition = initialChildPositions[index];
            index++;
        }
    }
}
