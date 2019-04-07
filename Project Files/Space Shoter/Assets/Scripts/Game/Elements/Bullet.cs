using UnityEngine;

public class Bullet : MonoBehaviour
{
    public enum BulletType { NONE, SIMPLE_BULLET, HOMING_MISSILE };

    private BulletType _type = BulletType.NONE;
    internal bool _wasFired = false;
    internal float _speed = 1;
    internal float _lifeTime = 5;
    internal float _damage = 0;
    internal Transform _target = null;

    public BulletType Type
    {
        get
        {
            return _type;
        }
    }

    private void Update()
    {
        if (!_wasFired)
            return;

        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
            Destroy(gameObject);
        else
            transform.localPosition += transform.forward * _speed;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Debug.Log("Explosion! Collides with: " + other.transform.parent.name + ", bullet: " + name);

        // TODO: show VFX of explosion

        // Checks if other object is damageable
        HealthController healthComponent = other.transform.parent.GetComponent<HealthController>();
        if (healthComponent != null)
            healthComponent.TakeDamage(_damage);

        Destroy(gameObject);
    }

    public virtual void Setup(float speedMovement, float lifetime, float damage, BulletType bulletType)
    {
        _type = bulletType;
        _speed = speedMovement;
        _lifeTime = lifetime;
        _damage = damage;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Fire()
    {
        _wasFired = true;
    }
}
