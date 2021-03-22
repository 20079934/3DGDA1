using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField]
    float amnt = 32;
    [SerializeField]
    float respawnTimer = 5;
    float timer = 0;

    BoxCollider bc;
    MeshRenderer mr;

    private void Start()
    {
        bc = GetComponent<BoxCollider>();
        mr = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (bc.enabled == false)
        {
            timer += Time.deltaTime;
            if (timer > respawnTimer)
            {
                timer = 0;
                bc.enabled = true;
                mr.enabled = true;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            other.gameObject.GetComponent<Ammo>().pickUp(amnt);
            bc.enabled = false;
            mr.enabled = false;
        }
    }
}
