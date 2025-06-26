using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = 1 << 8;
    [SerializeField] LayerMask layerMaskWalls = 1 << 8;
    [SerializeField] float rayDistance = 0.3f;
    [SerializeField] float floatDistance = 0.3f;
    [SerializeField] float pullSpeed = 5f;
    [SerializeField] float dropCheckRadius = 0.2f;
    [SerializeField] int layerToIgnore = 9; // The layer to ignore collisions with
    [SerializeField] float rotationSpeed = 50f; // Speed of rotation
    private Rigidbody2D pickedUpObject;

    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, (mouseWorldPosition - transform.position).normalized, rayDistance, layerMask);

        if (Input.GetButtonDown("Action"))
        {
            if (pickedUpObject != null)
            {
                // Restore collision when dropping the object
                Physics2D.IgnoreLayerCollision(pickedUpObject.gameObject.layer, layerToIgnore, false);
                pickedUpObject = null;
            }
            else if (hit.collider != null)
            {
                pickedUpObject = hit.transform.gameObject.GetComponent<Rigidbody2D>();
                if (pickedUpObject != null)
                {
                    // Ignore collision when picking up the object
                    Physics2D.IgnoreLayerCollision(pickedUpObject.gameObject.layer, layerToIgnore, true);
                }
            }
        }

        if (pickedUpObject != null)
        {
            pickedUpObject.linearVelocity = Vector2.zero;
            Vector3 direction = (mouseWorldPosition - transform.position).normalized;

            // Perform a raycast to find the target position
            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, direction, rayDistance, layerMaskWalls);
            Vector3 targetPosition;

            if (raycastHit.collider != null)
            {
                // If the raycast hits something, set targetPosition to the hit point
                targetPosition = raycastHit.point;
            }
            else
            {
                // If it doesn't hit anything, use the floatDistance
                targetPosition = transform.position + direction * floatDistance;
            }

            // Move the picked up object to the target position
            pickedUpObject.transform.position = targetPosition;

            // Handle rotation
            HandleRotation();
        }
    }

    private void HandleRotation()
    {
        if (Input.GetButton("Rotate")) // Replace "Rotate" with your desired input button
        {
            pickedUpObject.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
