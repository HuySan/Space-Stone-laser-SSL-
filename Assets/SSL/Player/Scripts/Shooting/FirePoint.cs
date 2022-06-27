using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FirePoint : MonoBehaviour
{   
    [SerializeField]
    private GameObject _bulletPrefab;
    private Laser _laser;

    private void Start()
    {       
        if (_bulletPrefab == null)
            throw new NullReferenceException("Bullet prefab not found");

        _laser = gameObject.GetComponent<Laser>();
        if(_laser == null)
            throw new NullReferenceException("Laser script not found");
    }


    public void Bullet() 
    {
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }

    public void Laser()
    {
        _laser.ShotLaser(transform.position);
    }


}
