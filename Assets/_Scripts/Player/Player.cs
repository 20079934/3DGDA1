using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Health health;
    int pickedUP = 0;
    float timer = 0;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    public void countUp()
    {
        pickedUP++;
        if(pickedUP>=10)
        {
            SceneManager.LoadScene("Win");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (health.health<0 || timer > 180)
        {
            SceneManager.LoadScene("Lose");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="NPC")
        {
            SceneManager.LoadScene("Lose");
        }
    }

}
