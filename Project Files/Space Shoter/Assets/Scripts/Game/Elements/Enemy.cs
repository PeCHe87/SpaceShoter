using System.Collections;
using UnityEngine;

public class Enemy : CharacterEntity
{
    [SerializeField] private ScriptableEnemy _data = null;

    private void Awake()
    {
        Health = GetComponent<HealthController>();
        Health.SetMaxHealth(_data.MaxHealth);
        Health.OnDead += Dead;
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
