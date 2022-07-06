using System;
using UnityEngine;
class GunsLogic
{
    private Transform _transform;
    private GameObject _targetLook;
    private float _speedRotation;
    public GunsLogic(Transform transform, GameObject targetLook, float speedRotation)
    {
        _transform = transform;
        _targetLook = targetLook;
        _speedRotation = speedRotation;
    }

    public void LookAtTarget()
    {
        Vector3 targetDirection = _transform.position - _targetLook.transform.position;
        float angle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        _transform.rotation = Quaternion.Slerp(_transform.rotation, rotation, _speedRotation * Time.deltaTime);
    }

    public void OrbitAround(Transform targetRotation)
    {
        if (targetRotation == null)
            return;
        _transform.RotateAround(targetRotation.position, Vector3.forward, _speedRotation * Time.deltaTime);
    }

    public bool CheckingEnemy(Transform firePoint, int distanceRay)
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, -_transform.up * distanceRay);
        //Debug.DrawRay(_firePoint.position, -transform.up * _distanceRay, Color.green);
        if (!hit)
            return false;

        IDamageible _iDamageible = hit.collider.GetComponent<IDamageible>();
        if (_iDamageible == null)
            return false;

        return true;
    }
}
