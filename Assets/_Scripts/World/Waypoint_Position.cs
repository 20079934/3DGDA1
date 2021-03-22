using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I only use this because when trying to get children of type transform is so infuriating.
public class Waypoint_Position : MonoBehaviour 
{
    private Vector3 _position;
    public Vector3 position { get => _position; set => _position = value; }

    private void Awake()
    {
        position = transform.position;
    }

}
