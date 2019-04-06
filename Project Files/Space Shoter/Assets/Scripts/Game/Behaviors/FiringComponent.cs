using System.Collections;
using UnityEngine;

public class FiringComponent : MonoBehaviour
{
    private bool _canFire = false;
    private bool _enable = false;
    private Weapon _weapon = null;
    private DetectionComponent _detectionComponent = null;
    private float _delayToStartFiring = 0;

    public void Setup(DetectionComponent detectionComponent, Weapon weapon, float delayToStartFiring)
    {
        _detectionComponent = detectionComponent;
        _detectionComponent.OnDetectedTarget += EnableFire;
        _detectionComponent.OnDetectedTargetLose += DisableFire;

        _weapon = weapon;

        _delayToStartFiring = delayToStartFiring;

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

        if (!_canFire)
            return;

        if (_weapon == null)
            return;

        _weapon.Fire();
    }

    private void OnDestroy()
    {
        if (_detectionComponent == null)
            return;

        _detectionComponent.OnDetectedTarget -= EnableFire;
        _detectionComponent.OnDetectedTargetLose -= DisableFire;
    }

    private void EnableFire(Transform target)
    {
        // If it was just enabled to fire then avoid this event and continue firing
        if (_canFire)
            return;

        StartCoroutine(CanFire());
    }

    private IEnumerator CanFire()
    {
        yield return new WaitForSeconds(_delayToStartFiring);

        _canFire = true;
    }

    private void DisableFire()
    {
        _canFire = false;
    }
}