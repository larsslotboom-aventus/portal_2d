using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] public PortalShooting PortalShooting;
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination;
    [SerializeField] private float teleportOffset;
    [SerializeField] private string allowedTag = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(allowedTag))
        {
            return;
        }

        // Check if the object is already in the portalObjects set
        if (portalObjects.Contains(collision.gameObject))
        {
            return;
        }

        // Teleport the object
        if (portalShooting.CanTeleport())
        {
            float angleDifference = transform.eulerAngles.z - destination.eulerAngles.z;
            Vector2 portalOffset = Quaternion.Euler(0, 0, angleDifference) * (collision.transform.position - transform.position);
            Vector2 teleportPosition = destination.position - destination.right * teleportOffset;
            teleportPosition += portalOffset * destination.transform.up;
            collision.transform.position = teleportPosition;

            portalObjects.Add(collision.gameObject);
        }

        if (destination.TryGetComponent(out Portal destinationPortal))
            {
                destinationPortal.portalObjects.Add(collision.gameObject);
            }

        Debug.Log($"{collision.gameObject.name} teleported to {destination.name}");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (portalObjects.Contains(collision.gameObject))
        {
            portalObjects.Remove(collision.gameObject);
            Debug.Log($"{collision.gameObject.name} exited the portal.");
        }
    }
}