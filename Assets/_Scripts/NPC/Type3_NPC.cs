using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type3_NPC : NPC
{
    [SerializeField]
    private float timeToShoot = 3;
    private float shootTimer = 0;

    private void Start()
    {
        enemyTarget = GameObject.FindGameObjectWithTag("Player"); //this will always be the target the enemy will try to shoot;
    }

    protected override void move()
    {
        if (info.IsName("Walking"))
        {
            anim.SetBool("canSeeTarget", true);
            anim.SetBool("canSmellTarget", true);
        }
        else if (info.IsName("Chasing"))
        {
            shootTimer += Time.deltaTime;
            setDestination(enemyTarget.transform.position);
            if (shootTimer > timeToShoot && anim.GetBool("canHearTarget")) //limit shooting to only hearing, close enough to player
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

        listen();
        requireAmmoSearch();
        requireHealthSearch();
    }
}
