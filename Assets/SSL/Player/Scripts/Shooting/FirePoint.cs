using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class FirePoint : MonoBehaviour, ICoroutineStartable
{   
    [SerializeField]
    private GameObject _bulletPrefab;

    //Laser options
    private Laser _laser;
    [SerializeField]
    private float _distanceRay = 100;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private GameObject _chargeGrid;
    [SerializeField]
    private float _timeToRecharge = 10;
    [SerializeField]
    private List<TextMeshProUGUI> _timeToRechargeText;
    [SerializeField]
    private LayerMask _hitLayerMask;
    private Image[] _laserCharges;

    private ICoroutineStartable _coroutineStartable;

    private void Start()
    {
        if (_bulletPrefab == null)
            throw new NullReferenceException("Bullet prefab not found");

        //-------Laser----------
        if (_lineRenderer == null)
            Debug.LogError("Error: the lineRenderer was not added");
        

        _laserCharges = _chargeGrid.GetComponentsInChildren<Image>();

        _timeToRechargeText.Reverse();

        _coroutineStartable = this.GetComponent<ICoroutineStartable>();

        _laser = new Laser(_coroutineStartable, _lineRenderer, this.transform, _hitLayerMask, _laserCharges, _timeToRechargeText);
        //------------------------
    }


    public void Bullet() 
    {
        Instantiate(_bulletPrefab, transform.position, transform.rotation);
    }

    public void Laser()
    {
        _laser.ShotLaser(transform.position, _distanceRay, _timeToRecharge);
    }

    public void StartChildCoroutine(IEnumerator corutineMethod)
    {
        StartCoroutine(corutineMethod);
    }
}
