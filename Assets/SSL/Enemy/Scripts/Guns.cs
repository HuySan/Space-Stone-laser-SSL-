using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour, IShootable
{
    [SerializeField]
    private Transform _targetRotation;
    [SerializeField]
    private float _speedRotation = 80;
    [SerializeField]
    private float _distanceRay = 100;
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private GameObject _bulletPrefab;

    private GameObject _targetLook;

    private IDamageible _iDamageible;

    //P.s. пули должны вылетать когда пушка смотрит на игрока(Будем чекать руйкастом скорее всего)
    private void Start()
    {
        _targetLook = EventBus.onObjectTransfed?.Invoke();
    }

    private void Update()
    {
        LookAtTarget();
        OrbitAround();
    }

    private void LookAtTarget()
    {
         Vector3 targetDirection =  this.transform.position - _targetLook.transform.position;
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _speedRotation * Time.deltaTime);
    }

    private void OrbitAround()
    {
        this.transform.RotateAround(_targetRotation.position, Vector3.forward, _speedRotation * Time.deltaTime);
    }

    public void Shoot()
    {
        if(CheckingEnemy())
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        //StartCoroutine(DelayBeforeShots(_delay));
    }

    private bool CheckingEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(_firePoint.position, -transform.up * _distanceRay);
        //Debug.DrawRay(_firePoint.position, -transform.up * _distanceRay, Color.green);
        if (!hit)
            return false;

        _iDamageible = hit.collider.GetComponent<IDamageible>();
        if(_iDamageible == null)
            return false;

        return true;
    }
}
