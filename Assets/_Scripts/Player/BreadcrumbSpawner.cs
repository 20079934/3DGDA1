using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadcrumbSpawner : MonoBehaviour
{

    Vector3 previousPosition;

    public static int counter = 0;
    public GameObject BC;

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLocation = transform.position;
        float distance = Vector3.Distance(previousPosition, currentLocation);
        if (distance > 1.0f)
        {
            previousPosition = currentLocation;
            GameObject g = Instantiate(BC, currentLocation, Quaternion.identity);
            g.name = "BC" + counter;
            counter++;
        }
    }
}
