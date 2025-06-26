using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float cameraDistance = 20f;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float cameraZoom = 20f;

    private Vector3 offset;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        offset = new Vector3(0, 0, -cameraDistance);
        transform.position = player.transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        cam.orthographicSize = cameraZoom;
    }
}