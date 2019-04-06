using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DetectionComponent))]
public class AimingComponent : MonoBehaviour
{
    [SerializeField] private Transform _body = null;

    private float _speedRotation = 0;
    private DetectionComponent _detectionComponent = null;
    private Transform _target = null;
    private bool _enable = false;

    private void OnDestroy()
    {
        if (_detectionComponent == null)
            return;

        _detectionComponent.OnDetectedTarget -= SetTarget;
        _detectionComponent.OnDetectedTargetLose -= CleanTarget;
    }

    public void Setup(DetectionComponent detectionComponent, float speedRotation)
    {
        _detectionComponent = detectionComponent;
        _detectionComponent.OnDetectedTarget += SetTarget;
        _detectionComponent.OnDetectedTargetLose += CleanTarget;

        _speedRotation = speedRotation;

        _enable = true;
    }

    public void Disable()
    {
        _enable = false;
    }

    private void Update()
    {
        if (!_enable)
            return;

        if (_target == null)
            return;

        // Rotates looking at target
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        Quaternion startRot = _body.rotation;
        Quaternion endRot = Quaternion.LookRotation(_target.position - _body.position, Vector3.up);

        _body.rotation = Quaternion.Slerp(startRot, endRot, _speedRotation * Time.deltaTime);
    }

    private void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }

    private void CleanTarget()
    {
        SetTarget(null);
    }
}
