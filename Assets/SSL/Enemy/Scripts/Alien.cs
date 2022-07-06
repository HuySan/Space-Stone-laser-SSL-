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
    private AlienLogic _alienLogic;

    void Start()
    {
        _defaultDirection = Vector2.down;
        _possibleDirection = new List<Vector2> { new Vector2(-1, -1), new Vector2(1, -1), Vector2.up, Vector2.left, Vector2.right };

        _iShootable = this.gameObject.GetComponentInChildren<IShootable>();
        if (_iShootable == null)
            Debug.LogError("_ishootable is null");

        _alienLogic = new AlienLogic(this.transform, _defaultDirection, _possibleDirection);
        InvokeRepeating("Shoot", 1, _delayForShooting);
    }

    void Update()
    {
        _alienLogic.CheckingCollision(_distanceChekingRay, _checkLayerMask);
        _alienLogic.Move(_speed);
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


    private void Shoot()
    {
        _iShootable.Shoot();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _alienLogic.Trigger(collision);
    }

    public void HitHandler()
    {
        Destroy(this.gameObject);
        EventBus.onAlienDeathChecked?.Invoke();
    }
}
