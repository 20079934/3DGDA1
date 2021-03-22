using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class NPC : MonoBehaviour
{
    [SerializeField]
    protected float healthRate = 5;
    [SerializeField]
    protected int _damage = 5;

    /// <summary>
    /// where the npc is going
    /// </summary>
    protected Vector3 currentTarget;
    protected Health health;
    protected Ammo ammo;
    protected NavMeshAgent nma;
    protected Animator anim;
    protected AnimatorStateInfo info;
    protected bool dead = false;
    /// <summary>
    /// player or npc that npc can damage
    /// </summary>
    protected GameObject enemyTarget;

    public int damage { get => _damage; set => _damage = value; }

    private void Awake()
    {
        nma = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        health = GetComponent<Health>();
        ammo = GetComponent<Ammo>();
    }

    private void Update()
    {
        info = anim.GetCurrentAnimatorStateInfo(0);
        if (!dead)
        {
            loseHealthOverTime();
            callBehaviours();
            move();
        }
    }

    

    void loseHealth(float amnt)
    {
        health.hurt(amnt);
        if (health.health < 0)
        {
            die();
        }
    }
    
    void loseHealthOverTime()
    {
        loseHealth(Time.deltaTime * healthRate);
    }
    public void attack(float amnt)
    {
        if(enemyTarget != null)
        {
            enemyTarget.GetComponent<Health>().hurt(amnt);
        }

    }

    void die()
    {
        dead = true;
        nma.isStopped = true;
        anim.SetBool("dead", dead);
    }

    

    /// <summary>
    /// Attach the senses and other behaviours on each child as required (e.g. look, listen, etc.)
    /// </summary>
    protected abstract void callBehaviours();


    /// <summary>
    /// This is where the the NPC will move depending on the state
    /// </summary>
    protected abstract void move();

    protected void setDestination(Vector3 dest)
    {
        currentTarget = dest;
        nma.SetDestination(currentTarget);
    }

    protected void lookForPlayer()
    {
        Ray ray = new Ray();
        RaycastHit hit;
        ray.origin = transform.position + Vector3.up * .7f;
        string objectInSight = "";
        float castingDistance = 20;
        ray.direction = transform.forward * castingDistance;
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, castingDistance))
        {
            objectInSight = hit.collider.gameObject.tag;
            if (objectInSight == "Player")
            {
                anim.SetBool("canSeeTarget", true);
            }
            else
            {
                anim.SetBool("canSeeTarget", false);
            }
        }
    }

    protected void listen()
    {
        if (enemyTarget != null)
        {
            float distance = Vector3.Distance(transform.position, enemyTarget.transform.position);
            if (distance < 3)
            {
                anim.SetBool("canHearTarget", true);
            }
            else
            {
                anim.SetBool("canHearTarget", false);
            }
        }
    }

    protected void smell()
    {
        GameObject[] allBC = GameObject.FindGameObjectsWithTag("Breadcrumb");
        float minDistance = 5;
        bool detectedBC = false;
        foreach (GameObject bc in allBC)
        {
            if (Vector3.Distance(bc.transform.position, transform.position) < minDistance)
            {
                detectedBC = true;
                break;
            }
        }
        anim.SetBool("canSmellTarget", detectedBC);
    }

    protected void requireAmmoSearch()
    {
        if (anim.GetBool("ammoBoxesAvailable"))
        {
            if (ammo.ammo < ammo.maxAmmo * 0.5)
            {
                anim.SetBool("requiresMoreAmmo", true);
            }
            else
            {
                anim.SetBool("requiresMoreAmmo", false);
            }
        }
    } 
    
    protected void requireHealthSearch()
    {
        if (anim.GetBool("healthPacksAvailable"))
        {
            if (health.health < 50)
            {
                anim.SetBool("requiresMoreHealth", true);
            }
            else
            {
                anim.SetBool("requiresMoreHealth", false);
            }
        }
    }
}
