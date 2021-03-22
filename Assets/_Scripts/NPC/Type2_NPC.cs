using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type2_NPC : NPC
{
    private Waypoint_Position currentWanderPoint;
    [SerializeField]
    private float timeToShoot = 3;
    private float shootTimer = 0;

    private void Start()
    {
        currentWanderPoint = new Waypoint_Position();
        wander();
        enemyTarget = GameObject.FindGameObjectWithTag("Player"); //this will always be the target the enemy will try to shoot;
    }

    protected override void move()
    {
        if (info.IsName("Walking"))
        {

            setDestination(currentWanderPoint);
            if (Vector3.Distance(gameObject.transform.position, currentTarget) < 2)
            {
                wander();
            }
        }
        else if (info.IsName("Chasing"))
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
        else if (info.IsName("LookForHealth"))
        {
            GameObject[] healthpacks;

            healthpacks = GameObject.FindGameObjectsWithTag("HealthPack");

            if (healthpacks.Length == 0) 
            { 
                //if none then ignore looking for a pack
                anim.SetBool("healthPacksAvailable", false); 
                anim.SetBool("requiresMoreHealth", false); 
            }
            else
            {
                //set the first found item and compare distance against every other item;
                setDestination(healthpacks[0].transform.position);
                float curr = Vector3.Distance(transform.position, currentTarget);
                foreach (GameObject r in healthpacks)
                {
                    if (Vector3.Distance(transform.position, r.transform.position) < curr)
                    {
                        setDestination(r.transform.position);
                    }
                }
            }
        }
        else if (info.IsName("LookForAmmo"))
        {
            GameObject[] ammoBoxes;

            ammoBoxes = GameObject.FindGameObjectsWithTag("AmmoBox");

            if (ammoBoxes.Length == 0)
            {
                //if none then ignore looking for a pack
                anim.SetBool("ammoBoxesAvailable", false);
                anim.SetBool("requiresMoreAmmo", false);
            }
            else
            {
                //set the first found item and compare distance against every other item;
                setDestination(ammoBoxes[0].transform.position);
                float curr = Vector3.Distance(transform.position, currentTarget);
                foreach (GameObject r in ammoBoxes)
                {
                    if (Vector3.Distance(transform.position, r.transform.position) < curr)
                    {
                        setDestination(r.transform.position);
                    }
                }
            }
        }

    }

    protected override void callBehaviours()
    {
        lookForPlayer();
        listen();
        smell();
        requireAmmoSearch();
        requireHealthSearch();
    }

    protected void setDestination(Waypoint_Position wp)
    {
        currentWanderPoint = wp;
        setDestination(wp.position); //reduction of code repetition
    }

    void wander()
    {
        Ray ray = new Ray();
        RaycastHit hit;

        ray.origin = transform.position + Vector3.up * .7f;
        float distanceToObstacle = 0;
        float castingDistance = 20;

        //do
        //{
        float randomDirectionX = UnityEngine.Random.Range(-1, 1);
        float randomDirectionZ = UnityEngine.Random.Range(-1, 1);

        ray.direction = transform.forward * randomDirectionZ + transform.right * randomDirectionX;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);
        //} while (distanceToObstacle < 1.0f);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
        {
            distanceToObstacle = hit.distance;
        }
        else
        {
            distanceToObstacle = castingDistance;
        }

        currentWanderPoint.position = ray.origin + ray.direction * (distanceToObstacle - 1);

        setDestination(currentTarget);
    }

}
