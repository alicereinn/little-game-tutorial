using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraMovement : MonoBehaviour
{
    private Camera camera;
    public float smoothing;
    // Player to follow
    public Transform target;
    private Vector3 targetPosition;
    // Tilemap bounds
    public Tilemap ground;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    private Vector3 dynamicMinBounds;
    private Vector3 dynamicMaxBounds;
    // Start is called before the first frame update
    void Start()
    {
        camera = gameObject.GetComponent<Camera>();
        GetCameraBounds();
        GetMapBounds();
    }

    void FixedUpdate()
    {
        GetCameraBounds();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        targetPosition.x = Mathf.Clamp(transform.position.x, dynamicMinBounds.x, dynamicMaxBounds.x);
        targetPosition.y = Mathf.Clamp(transform.position.y, dynamicMinBounds.y, dynamicMaxBounds.y);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        GetCameraBounds();
        // if (transform.position != target.position)
        // {
        //     targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        //     targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        //     targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
        //     transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        // }
        
    }

    // These stay the same during gameplay
    void GetMapBounds()
    {
        Debug.Log("Bound: " + ground.localBounds);
        minBounds = ground.transform.TransformPoint(ground.localBounds.min);
        maxBounds = ground.transform.TransformPoint(ground.localBounds.max);
        Debug.Log("Transform (min): " + ground.transform.TransformPoint(ground.localBounds.min));
        Debug.Log("Transform (max): " + ground.transform.TransformPoint(ground.localBounds.max));
    }

    // These can change as the player resizes the window
    void GetCameraBounds()
    {

        float camExtentV = camera.orthographicSize;  
        float camExtentH = camExtentV * Screen.width / Screen.height;

        float leftBound = minBounds.x + camExtentH;
        float rightBound = maxBounds.x - camExtentH;
        float bottomBound = minBounds.y + camExtentV;
        float topBound = maxBounds.y - camExtentV;

        dynamicMinBounds = minBounds + new Vector3(camExtentH, camExtentV);
        dynamicMaxBounds = maxBounds - new Vector3(camExtentH, camExtentV);


        Debug.Log("Height: " + camera.pixelHeight);
        Debug.Log("Dynamic height: " + camera.scaledPixelHeight);
        Debug.Log("Width: " + camera.pixelWidth);
        Debug.Log("Dynamic width: " + camera.scaledPixelWidth);
        Debug.Log("Rect: " + camera.rect);
        Debug.Log("Rect: " + camera.pixelRect);
        Debug.Log("Screen Height: " + (float)Screen.width);
        Debug.Log("Screen Width: " + (float)Screen.height);
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Debug.Log(new Vector2(camera.orthographicSize * Screen.width/Screen.height, camera.orthographicSize));

    }
}
