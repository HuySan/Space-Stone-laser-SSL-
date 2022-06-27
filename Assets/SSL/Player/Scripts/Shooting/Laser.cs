using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] 
    private float _distanceRay = 100;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private int _shotsCount = 3;
    private int _maxShotCount;
    [SerializeField]
    private float _timeToRecharge = 20;
    [SerializeField]
    private LayerMask _hitLayerMask;

    private void Start()
    {        
        if (_lineRenderer == null)
            Debug.LogError("Error: the lineRenderer was not added");
        _maxShotCount = _shotsCount;
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
            /* Asteroid asteroid = hit.collider.gameObject.GetComponent<Asteroid>();
             if (asteroid != null)
                 asteroid.ObjectDecay();*/
            IHitable iHitable = hit.collider.gameObject.GetComponent<IHitable>();
            if (iHitable != null)
                iHitable.HitHandler();
        }

        StartCoroutine(DelayRay());
        _shotsCount--;
        StartCoroutine(ChargeAccumulation());
    }

    IEnumerator DelayRay()
    {
        yield return new WaitForSeconds(0.05f);
        _lineRenderer.enabled = false;
    }

    IEnumerator ChargeAccumulation()
    {
//Заряды набираются одновременно, т.к. я выстрелил поочерёдно.Можно к каждому последуещему заряду прибавлять время предыдущего, тогда заряды будут долше наполняться
        if (_shotsCount >= _maxShotCount)
             yield break;

        yield return new WaitForSeconds(_timeToRecharge);
        _shotsCount++;
    }

    private void Draw2DRay(Vector2 startPosition, Vector2 endPosition)
    {
        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(1, endPosition);
    }

}
