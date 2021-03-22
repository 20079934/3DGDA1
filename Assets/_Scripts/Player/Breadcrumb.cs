using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breadcrumb : MonoBehaviour
{
    [SerializeField]
    float ttl = 5;

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0)
        {
            BreadcrumbSpawner.counter--;
            Destroy(gameObject);
        }
    }
}
