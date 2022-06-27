using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour, IMovable, IShooting, IDamageible
{
    [SerializeField]
    private float _speed = 0;
    [SerializeField]
    private float _maxSpeed = 7;
    [SerializeField]
    private float _acceleration = 1.6f;
    private Vector2 _directionMove;
    [SerializeField]
    private float _inertia = 2.5f;
    [SerializeField]
    private float _speedRotate = 10f;
    [SerializeField]
    private FirePoint _firePoint;

    private Vector2 _targetDirection;

    public void LookAtMouse(Vector2 positionMouse)
    {
        _targetDirection = Camera.main.ScreenToWorldPoint(positionMouse) - transform.position;
        float angle = Mathf.Atan2(_targetDirection.x, _targetDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedRotate * Time.deltaTime);
    }

    public void MoveForward(Vector2 direction)
    {
          _directionMove = direction;

          if (_speed < _maxSpeed)
              _speed += _acceleration * Time.deltaTime;

          transform.Translate(direction *  _speed * Time.deltaTime);
    }

    public void MoveForward(bool isPressed)
    {
        if (isPressed)
            return;

        if (_speed <= 0)
            return;

        _speed -= _inertia * Time.deltaTime;
        transform.Translate(_directionMove * _speed * Time.deltaTime);
    }

    public void ShootBullet()
    {
        _firePoint.Bullet();
    }

    public void ShootLaser()
    {
        _firePoint.Laser();
    }

    public void InflictDamage()
    {
        Destroy(this.gameObject);
        //спавним эффект
        //Вызываем скрипт с проигрыванием аудио(Думаю сделать всю работу аудио в отдельном скрипте и из него вызывать методы)
    }

    private void OnEnable()
    {
        EventBus.onObjectTransfed += Init;
    }

    private void OnDisable()
    {
        EventBus.onObjectTransfed -= Init;
    }

    private GameObject Init()
    {
        return this.gameObject;
    }

}
