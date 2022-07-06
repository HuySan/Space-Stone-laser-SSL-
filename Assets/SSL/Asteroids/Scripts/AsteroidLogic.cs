using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic
{
    private Rigidbody2D _rigidbody;

    private List<Vector2> _vectorDirection;

    private IDamageible _iDamageible;


    public AsteroidLogic(Rigidbody2D rigidbody, List<Vector2> vectorDirection)
    {
        _rigidbody = rigidbody;
        _vectorDirection = vectorDirection;
    }


    public void Move(float minForce, float maxForce)
    {
        int randomVector = Random.Range(0, 5);
        _rigidbody.AddForce(_vectorDirection[randomVector] * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
    }

    public void OnTrigger(Collider2D collision)//hit player
    {
        _iDamageible = collision.gameObject.GetComponent<IDamageible>();
        
        if (_iDamageible == null)
            return;
        _iDamageible.InflictDamage();
    }
}
