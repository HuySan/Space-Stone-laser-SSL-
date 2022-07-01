using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour, IHitable
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private int _distanceChekingRay = 100;

    private Vector3 _defaultDirection;
    private List<Vector2> _possibleDirection;
    [SerializeField]
    private LayerMask _checkLayerMask;

    private IShootable _iShootable;

    [SerializeField]
    private float _delayForShooting = 0.7f;


    void Start()
    {
        _defaultDirection = Vector2.down;
        _possibleDirection = new List<Vector2> { new Vector2(-1, -1), new Vector2(1, -1), Vector2.up, Vector2.left, Vector2.right };

        _iShootable = this.gameObject.GetComponentInChildren<IShootable>();
        if (_iShootable == null)
            Debug.LogError("_ishootable is null");

        InvokeRepeating("Shoot", 1, _delayForShooting);
    }

    void Update()
    {
        CheckingCollision();
        Move();
    }

    //For clarity

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, new Vector2(1, 1), 90f, _defaultDirection, _distanceChekingRay);
        if (!hit)
            return;

        Gizmos.DrawRay(this.transform.position, _defaultDirection * hit.distance);
        Gizmos.DrawWireCube(this.transform.position + _defaultDirection * hit.distance, new Vector2(1, 1));
    }*/

    private void Move()
    {
        this.gameObject.transform.Translate(_defaultDirection * _speed * Time.deltaTime);
    }

    private void Shoot()
    {
        _iShootable.Shoot();
    }


    private void CheckingCollision()
    {
        if (!Physics2D.BoxCast(this.transform.position, new Vector2(1, 1), 90f, _defaultDirection, _distanceChekingRay, _checkLayerMask))
            return;

        foreach (Vector2 direction in _possibleDirection)
        {
            _defaultDirection = direction;
            RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, new Vector2(1, 1), 90f, _defaultDirection, _distanceChekingRay, _checkLayerMask);
            if (hit.collider)
                continue;
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageible iDamageible = collision.gameObject.GetComponent<IDamageible>();
        if (iDamageible == null)
            return;
        iDamageible.InflictDamage();
    }

    public void HitHandler()
    {
        Destroy(this.gameObject);
        EventBus.onAlienDeathChecked?.Invoke();
    }
}
