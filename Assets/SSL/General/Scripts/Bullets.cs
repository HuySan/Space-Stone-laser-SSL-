using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 20f;
    [SerializeField]
    private float _lifeTime = 1.7f;
    [SerializeField]
    private Vector3 _translation;


    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, 90));
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(_translation * _moveSpeed * Time.deltaTime);
    }

}
