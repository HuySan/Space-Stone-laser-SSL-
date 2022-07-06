using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Laser
{
    private LineRenderer _lineRenderer;
    private Transform _transform;
    private LayerMask _hitLayerMask;
    private Image[] _laserCharges;
    private List<TextMeshProUGUI> _timeToRechargeText;

    private float _timeToRecharge;

    private int _shotsCount = 3;
    private int _maxShotCount;

    ICoroutineStartable _coroutineStartable;

    public Laser(ICoroutineStartable coroutineStartable, LineRenderer lineRenderer, Transform transform, LayerMask hitLayerMask, Image[] laserCharges, List<TextMeshProUGUI> timeToRechargeText)
    {
        _lineRenderer = lineRenderer;
        _transform = transform;
        _hitLayerMask = hitLayerMask;
        _laserCharges = laserCharges;
        _timeToRechargeText = timeToRechargeText;
        _coroutineStartable = coroutineStartable;

        _maxShotCount = _shotsCount;
    }

    public void ShotLaser(Vector3 shootPosition, float distanceRay, float timeToRecharge)
    {
        _timeToRecharge = timeToRecharge;

        if (_shotsCount == 0)
            return;

        _lineRenderer.enabled = true;

        RaycastHit2D[] hits = Physics2D.RaycastAll(shootPosition, _transform.up * distanceRay, distanceRay, _hitLayerMask);       
        Draw2DRay(shootPosition, _transform.up * distanceRay);
        foreach(RaycastHit2D hit in hits)
        {

            if (hit.collider == null)
                continue;

            IHitable iHitable = hit.collider.gameObject.GetComponent<IHitable>();
            if (iHitable != null)
                iHitable.HitHandler();
        }


        _coroutineStartable.StartChildCoroutine(DelayRay());

        _shotsCount--;
        _laserCharges[_shotsCount].gameObject.SetActive(false);
        //Debug.Log(_shotsCount);

        _coroutineStartable.StartChildCoroutine(ChargeAccumulation(_timeToRechargeText[_shotsCount], _shotsCount));
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
        if (shotsCount == 0)
            _shotsCount = _maxShotCount;
    }

    private void Draw2DRay(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }

}
