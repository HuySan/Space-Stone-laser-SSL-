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
    private int _distanceRay = 100;
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private GameObject _bulletPrefab;
    private GameObject _targetLook;

    private GunsLogic _gunsLogic;

    private void Start()
    {
        _targetLook = EventBus.onObjectTransfed?.Invoke();
        _gunsLogic = new GunsLogic(this.transform, _targetLook, _speedRotation);
    }

    private void Update()
    {
        _gunsLogic.LookAtTarget();
        _gunsLogic.OrbitAround(_targetRotation);
    }

    public void Shoot()
    {
        if(_gunsLogic.CheckingEnemy(_firePoint, _distanceRay))
            Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
    }

}
