using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class MoverLogic 
{
    private float _speed, _maxSpeed, _speedRotate, _acceleration, _inertia;
    private Transform _transform;

    private Vector2 _targetDirection;
    private Vector2 _directionMove;
    public MoverLogic(float speed, float maxSpeed, float speedRotate, float acceleration, float inertia, Transform transform)
    {
        _speed = speed;
        _maxSpeed = maxSpeed;
        _transform = transform;
        _speedRotate = speedRotate;
        _acceleration = acceleration;
        _inertia = inertia;
    }

    public void LookAtMouse(Vector2 positionMouse)
    {
        _targetDirection = Camera.main.ScreenToWorldPoint(positionMouse) - _transform.position;
        float angle = Mathf.Atan2(_targetDirection.x, _targetDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, _speedRotate * Time.deltaTime);
    }


    public void MoveForward(Vector2 direction, out float speed)
    {
        _directionMove = direction;

        if (_speed < _maxSpeed)
            _speed += _acceleration * Time.deltaTime;
        _transform.Translate(direction * _speed * Time.deltaTime);
        speed = _speed;
    }

    public void MoveForward(bool isPressed, out float speed)
    {
        speed = _speed;
        if (isPressed)
            return;

        if (_speed <= 0)
            return;

        _speed -= _inertia * Time.deltaTime;
        speed = _speed;
        _transform.Translate(_directionMove * _speed * Time.deltaTime);
    }

}
