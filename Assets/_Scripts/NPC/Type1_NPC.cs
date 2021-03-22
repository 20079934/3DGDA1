using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Type1_NPC : NPC
{
    [SerializeField]
    private GameObject waypointRoot;
    private Waypoint_Position[] waypoints;
    private Waypoint_Position currentWaypoint;
    [SerializeField]
    private float timeToShoot = 3;
    private float shootTimer = 0;

    private void Start()
    {
        waypoints = waypointRoot.GetComponentsInChildren<Waypoint_Position>();
        setDestination(waypoints[Random.Range(0, waypoints.Length)]);
        enemyTarget = GameObject.FindGameObjectWithTag("Player"); //this will always be the target the enemy will try to shoot;
    }

    protected override void move()
    {
        if (info.IsName("Walking"))
        {
            
            setDestination(currentWaypoint);
            if (Vector3.Distance(gameObject.transform.position, currentTarget) < 2)
            {
                setDestination(waypoints[Random.Range(0, waypoints.Length)]);
            }
        }
        else if(info.IsName("Chasing"))
        {
            shootTimer += Time.deltaTime;
            setDestination(enemyTarget.transform.position);
            if (shootTimer > timeToShoot)
            {
                shootTimer = 0;
                if (ammo.ammo > 0) //eventual collection of ammo
                {
                    ammo.fire(1);
                    anim.SetTrigger("readyToFire");
                }
            }
        }

    }

    protected override void callBehaviours()
    {
        lookForPlayer();
        listen();
        smell();
    }

    protected void setDestination(Waypoint_Position wp)
    {
        currentWaypoint = wp;
        setDestination(wp.position); //reduction of code repetition
    }
    

}
