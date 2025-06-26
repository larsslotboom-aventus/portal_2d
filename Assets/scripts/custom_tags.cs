using System.Collections.Generic;
using UnityEngine;

public class cubes : MonoBehaviour
{
    [System.Flags]
    public enum tags
    {
        standable = 1,
        pickupable = 2,
    }
    public tags CustomTags;
}
