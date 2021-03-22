using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField]
    float _health = 100;

    float maxHealth;

    void Start()
    {
        maxHealth = health;
    }

    public float health { get => _health; set => _health = value; }

    public void hurt(float amnt)
    {
        _health -= amnt;
    }

    public void heal(float amnt)
    {
        _health += amnt;
        _health =  _health > maxHealth ? maxHealth : _health;
    }
}
