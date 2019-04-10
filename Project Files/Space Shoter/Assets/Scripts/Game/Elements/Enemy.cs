using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DetectionComponent))]
[RequireComponent(typeof(AimingComponent))]
public class Enemy : CharacterEntity
{
    [SerializeField] private ScriptableEnemy _data = null;

    private DetectionComponent _detectionComponent = null;
    private AimingComponent _aimingComponent = null;
    private FiringComponent _firingComponent = null;
    private Weapon _weapon = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();

        if (Health != null)
        {
            Health.SetMaxHealth(_data.MaxHealth);
            Health.OnDead += Dead;
        }

        _detectionComponent = GetComponent<DetectionComponent>();

        _aimingComponent = GetComponent<AimingComponent>();

        _firingComponent = GetComponent<FiringComponent>();

        _weapon = GetComponent<Weapon>();

        GameEventsManager.OnPlayerDead += DeactivateComponents;
    }

    private void Start()
    {
        // Setup detection component with info from data
        _detectionComponent.Setup(_data.DetectionMask, _data.DetectionRadius);

        // Setup aiming component
        _aimingComponent.Setup(_detectionComponent, _data.SpeedRotation);

        // Setup firing component
        if (_weapon != null && _firingComponent != null)
            _firingComponent.Setup(_detectionComponent, _weapon, _data.DelayToStartFiring);
    }

    private void OnDestroy()
    {
        Health.OnDead -= Dead;

        GameEventsManager.OnPlayerDead -= DeactivateComponents;
    }

    private void Dead()
    {
        DeactivateComponents();

        StartCoroutine(DestroyEntity());
    }

    private IEnumerator DestroyEntity()
    {
        // TODO: Show explosion VFX

        yield return new WaitForEndOfFrame();

        HideGraphics();

        yield return new WaitForSeconds(_data.DeadDelay);

        // TODO: back to pool when there were one, replace this destroy object line
        Destroy(gameObject);
    }

    private void DeactivateComponents()
    {
        // Disable aiming
        _aimingComponent.Disable();

        // Disable detection
        _detectionComponent.Disable();

        // Disable firing 
        if (_firingComponent != null)
            _firingComponent.Disable();
    }
}
