using UnityEngine;

public class PortalShooting : MonoBehaviour
{
    [SerializeField] LayerMask layerMask = 1 << 8;
    private GameObject portalBlue;
    private GameObject portalOrange;
    private bool bluePortalFired = false;
    private bool orangePortalFired = false;

    void Start()
    {
        portalBlue = GameObject.Find("portal blue");
        portalOrange = GameObject.Find("portal orange");

        if (portalBlue != null)
        {
            portalBlue.SetActive(false);
        }

        if (portalOrange != null)
        {
            portalOrange.SetActive(false);
        }
    }

    Quaternion GetRotation(Vector2 hitpoint, Vector2 rayDirection)
    {
        hitpoint += -rayDirection.normalized * 0.001f;
        RaycastHit2D topleft = Physics2D.Raycast(hitpoint, new Vector2(-1, 1), 0.01f, layerMask);
        RaycastHit2D topright = Physics2D.Raycast(hitpoint, new Vector2(1, 1), 0.01f, layerMask);
        RaycastHit2D bottomleft = Physics2D.Raycast(hitpoint, new Vector2(-1, -1), 0.01f, layerMask);
        RaycastHit2D bottomright = Physics2D.Raycast(hitpoint, new Vector2(1, -1), 0.01f, layerMask);

        if (topleft.collider != null && topright.collider != null)
            return Quaternion.Euler(0, 0, 90);
        else if (bottomleft.collider != null && bottomright.collider != null)
            return Quaternion.Euler(0, 0, -90);
        else if (topleft.collider != null && bottomleft.collider != null)
            return Quaternion.Euler(0, 0, 180);
        else if (topright.collider != null && bottomright.collider != null)
            return Quaternion.Euler(0, 0, 0);
        
        return Quaternion.Euler(0, 0, 0);
    }

    void Update()
    {
        HandlePortalShooting(0, ref portalBlue, ref bluePortalFired);
        HandlePortalShooting(1, ref portalOrange, ref orangePortalFired);
    }

    void HandlePortalShooting(int mouseButton, ref GameObject portal, ref bool portalFired)
    {
        if (Input.GetMouseButtonDown(mouseButton))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, 
                mouseWorldPos - transform.position, 100f, layerMask);

            if (hit.collider != null)
            {
                if (portal != null)
                {
                    portal.transform.position = hit.point;
                    portal.transform.rotation = GetRotation(hit.point, 
                        mouseWorldPos - transform.position);
                    
                    if (!portal.activeSelf)
                    {
                        portal.SetActive(true);
                        portalFired = true;
                    }
                }
            }
        }
    }

    // Call this from your teleportation script to check if teleport is allowed
    public bool CanTeleport()
    {
        return bluePortalFired && orangePortalFired;
    }
}
