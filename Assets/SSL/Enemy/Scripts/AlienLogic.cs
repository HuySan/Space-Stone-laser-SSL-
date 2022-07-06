using System;
using System.Collections.Generic;
using UnityEngine;

class AlienLogic
{
    private Transform _transform;
    private Vector3 _defaultDirection;

    private List<Vector2> _possibleDirection;

    public AlienLogic(Transform transform, Vector3 defaultDirection, List<Vector2> possibleDirection)
    {
        _transform = transform;
        _defaultDirection = defaultDirection;
        _possibleDirection = possibleDirection;
    }

    public void Move(float speed)
    {
        _transform.Translate(_defaultDirection * speed * Time.deltaTime);
    }

    public void CheckingCollision(int distanceChekingRay, LayerMask checkLayerMask)
    {
        if (!Physics2D.BoxCast(_transform.position, new Vector2(1, 1), 90f, _defaultDirection, distanceChekingRay, checkLayerMask))
            return;

        foreach (Vector2 direction in _possibleDirection)
        {
            _defaultDirection = direction;
            RaycastHit2D hit = Physics2D.BoxCast(_transform.position, new Vector2(1, 1), 90f, _defaultDirection, distanceChekingRay, checkLayerMask);
            if (hit.collider)
                continue;
            return;
        }
    }

    public void Trigger(Collider2D collision)
    {
        IDamageible iDamageible = collision.gameObject.GetComponent<IDamageible>();
        if (iDamageible == null)
            return;
        iDamageible.InflictDamage();
    }
}
