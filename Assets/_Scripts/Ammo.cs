using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField]
    float _ammo = 64;

    float _maxAmmo;

    void Start()
    {
        maxAmmo = ammo;
    }

    public float ammo { get => _ammo; set => _ammo = value; }
    public float maxAmmo { get => _maxAmmo; set => _maxAmmo = value; }

    public void fire(float amnt)
    {
        _ammo -= amnt;
    }

    public void pickUp(float amnt)
    {
        _ammo += amnt;
        _ammo = _ammo > maxAmmo ? maxAmmo : _ammo;
    }
}
