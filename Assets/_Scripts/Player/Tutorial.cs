using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    GameObject wall;
    float startPosX, startPosZ;
    private void Start()
    {
        startPosX = transform.position.x;
        startPosZ = transform.position.z;
    }

    void Update()
    {
        if(startPosX != transform.position.x || startPosZ != transform.position.z)
        {
            Destroy(wall);
            Destroy(this);
        }
    }
}
