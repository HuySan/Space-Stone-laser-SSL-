using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Asteroid : MonoBehaviour, IHitable
{
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float _MaxForce = 1.4f;
    [SerializeField]
    private float _MinForce = 0.5f;
    private List<Vector2> _vectorDirection;

    private IDamageible _iDamageible;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _vectorDirection = new List<Vector2> { Vector2.up, Vector2.one, -Vector2.one, Vector2.right, Vector2.left, Vector2.down };
        Move();
    }

    private void Move()
    {
        int randomVector = Random.Range(0, 5);
        _rigidbody.AddForce(_vectorDirection[randomVector] * Random.Range(_MinForce, _MaxForce), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)//hit player
    {
        _iDamageible = collision.gameObject.GetComponent<IDamageible>();
        
        if (_iDamageible == null)
            return;
        _iDamageible.InflictDamage();
    }

   // public virtual void ObjectDecay(){ }//hit asteroid

    public virtual void HitHandler()
    {
        throw new System.NotImplementedException();
    }
}
