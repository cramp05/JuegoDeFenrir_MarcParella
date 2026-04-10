using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform cameraTarget;
    public Vector3 cameraOffset;

    public Vector2 minCameraPosition;
    public Vector2 maxCameraPosition;

    void Awake()
    {
        //cameraTarget = GameObject.Find("Mario").GetComponent<Transform>();
        cameraTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }


    // Update is called once per frame
    void Update()
    {
        if (cameraTarget != null)
        {
            //transform.position = new Vector3(cameraTarget.position.x, 0, 0) + cameraOffset;
            Vector3 desiredPosition = cameraTarget.position + cameraOffset;

            float clampX = Mathf.Clamp(desiredPosition.x, minCameraPosition.x, maxCameraPosition.x);
            float clampY = Mathf.Clamp(desiredPosition.y, minCameraPosition.y, maxCameraPosition.y);

            Vector3 clampedPosition = new Vector3(clampX, clampY, desiredPosition.z);

            transform.position = clampedPosition;
        }

    }
}
