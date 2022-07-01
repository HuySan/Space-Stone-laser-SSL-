using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Laser : MonoBehaviour
{
    [SerializeField] 
    private float _distanceRay = 100;
    [SerializeField]
    private LineRenderer _lineRenderer;

    private int  _shotsCount = 3;
    [SerializeField]
    private GameObject _chargeGrid;
    private Image[] _laserCharges;

    private int _maxShotCount;
    [SerializeField]
    private float _timeToRecharge = 10;
    [SerializeField]
    private List<TextMeshProUGUI> _timeToRechargeText;


    [SerializeField]
    private LayerMask _hitLayerMask;

    private void Start()
    {        
        if (_lineRenderer == null)
            Debug.LogError("Error: the lineRenderer was not added");
        _maxShotCount = _shotsCount;

        _laserCharges = _chargeGrid.GetComponentsInChildren<Image>();

        _timeToRechargeText.Reverse();
    }

    public void ShotLaser(Vector3 shootPosition)
    {
        if (_shotsCount == 0)
            return;

        _lineRenderer.enabled = true;

        RaycastHit2D[] hits = Physics2D.RaycastAll(shootPosition, transform.up * _distanceRay, _distanceRay, _hitLayerMask);       
        Draw2DRay(shootPosition, transform.up * _distanceRay);
        foreach(RaycastHit2D hit in hits)
        {

            if (hit.collider == null)
                continue;

            IHitable iHitable = hit.collider.gameObject.GetComponent<IHitable>();
            if (iHitable != null)
                iHitable.HitHandler();
        }

        StartCoroutine(DelayRay());
        _shotsCount--;
        _laserCharges[_shotsCount].gameObject.SetActive(false);

        StartCoroutine(ChargeAccumulation(_timeToRechargeText[_shotsCount], _shotsCount));
    }

    IEnumerator DelayRay()
    {
        yield return new WaitForSeconds(0.1f);
        _lineRenderer.enabled = false;
    }

    IEnumerator ChargeAccumulation(TextMeshProUGUI timeText, int shotsCount)
    {
        if (_shotsCount >= _maxShotCount)
             yield break;
        float temp = _timeToRecharge;

        while (temp >= 0)
        {
            timeText.text = temp.ToString();
            yield return new WaitForSeconds(1);
            temp--;
        }

        _laserCharges[shotsCount].gameObject.SetActive(true);
        _shotsCount++;
    }

    private void Draw2DRay(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }

}
