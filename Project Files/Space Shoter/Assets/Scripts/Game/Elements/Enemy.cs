using System.Collections;
using UnityEngine;

public class Enemy : CharacterEntity
{
    [SerializeField] private ScriptableEnemy _data = null;

    private DetectionComponent _detectionComponent = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_data.MaxHealth);
        Health.OnDead += Dead;

        _detectionComponent = GetComponent<DetectionComponent>();
    }

    private void Start()
    {
        // If it has a detection component attached then setup it with info from data
        if (_detectionComponent != null)
            _detectionComponent.Setup(_data.DetectionMask, _data.DetectionRadius);
    }

    private void OnDestroy()
    {
        Health.OnDead -= Dead;
    }

    private void Dead()
    {
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
}
