using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public Camera cam; // Reference to the Camera component
    public string targetTag = "Singing"; // Tag to find the target
    public float zoomedSize = 3f; // Zoomed-in orthographic size
    public float normalSize = 5f; // Default orthographic size
    public float zoomSpeed = 5f; // Speed of zooming
    public float moveSpeed = 5f; // Speed of camera movement

    private Vector3 originalPosition;
    private bool isZoomed = false;

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main; // Assign main camera if not set
        }
        originalPosition = cam.transform.position;
    }

    void Update()
    { 
            StopAllCoroutines();
            if (isZoomed)
            {
                StartCoroutine(ZoomCamera(normalSize, originalPosition));
                isZoomed = false;
            }
            else
            {
                GameObject targetObject = GameObject.FindGameObjectWithTag(targetTag);
                if (targetObject != null)
                {
                    Vector3 zoomedPosition = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y + 1.5f, cam.transform.position.z);
                    StartCoroutine(ZoomCamera(zoomedSize, zoomedPosition));
                    isZoomed = false;
                }
            }
        }

    System.Collections.IEnumerator ZoomCamera(float targetSize, Vector3 targetPosition)
    {
        while (Mathf.Abs(cam.orthographicSize - targetSize) > 0.01f || Vector3.Distance(cam.transform.position, targetPosition) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
            cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        cam.orthographicSize = targetSize;
        cam.transform.position = targetPosition;
    }
}