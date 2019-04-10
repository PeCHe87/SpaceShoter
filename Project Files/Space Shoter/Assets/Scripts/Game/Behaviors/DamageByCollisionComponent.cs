using UnityEngine;

[RequireComponent(typeof(PushBackComponent))]
public class DamageByCollisionComponent : MonoBehaviour
{
    private PushBackComponent _pushBackComponent = null;
    private HealthController _health = null;
    private float _damageByCollision = 2;

    public float DamageByCollision
    {
        set
        {
            _damageByCollision = value;
        }
    }

    private void Awake()
    {
        _pushBackComponent = GetComponent<PushBackComponent>();
        _pushBackComponent.OnCollision += CollisionDetected;

        _health = GetComponent<HealthController>();
    }

    private void OnDestroy()
    {
        _pushBackComponent.OnCollision -= CollisionDetected;
    }

    private void CollisionDetected()
    {
        if (_health == null)
            return;

        _health.TakeDamage(_damageByCollision);
    }
}