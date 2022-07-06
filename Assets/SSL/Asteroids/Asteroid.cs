using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

 public abstract class Asteroid : MonoBehaviour, IHitable
{
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _maxForce = 1.4f;
    [SerializeField]
    private float _minForce = 0.5f;
    private List<Vector2> _vectorDirection;

    private AsteroidLogic _asteroidLogic;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _vectorDirection = new List<Vector2> { Vector2.up, Vector2.one, -Vector2.one, Vector2.right, Vector2.left, Vector2.down };
        _asteroidLogic = new AsteroidLogic(_rigidbody, _vectorDirection);
        Move();
    }

    private void Move()
    {
        _asteroidLogic.Move(_minForce, _maxForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _asteroidLogic.OnTrigger(collision);
    }

    public virtual void HitHandler()
    {
        throw new NotImplementedException();
    }
}