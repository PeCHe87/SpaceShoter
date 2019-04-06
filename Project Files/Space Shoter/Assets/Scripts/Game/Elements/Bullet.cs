using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _wasFired = false;
    private float _speed = 1;
    private float _lifeTime = 5;
    private float _damage = 0;

    private void Update()
    {
        if (!_wasFired)
            return;

        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
            Destroy(gameObject);
        else
        {
            transform.localPosition += transform.forward * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Explosion! Collides with: " + other.transform.parent.name + ", bullet: " + name);

        // TODO: show VFX of explosion

        // Checks if other object is damageable
        HealthController healthComponent = other.transform.parent.GetComponent<HealthController>();
        if (healthComponent != null)
            healthComponent.TakeDamage(_damage);

        Destroy(gameObject);
    }

    public void Setup(float speedMovement, float lifetime, float damage)
    {
        _speed = speedMovement;
        _lifeTime = lifetime;
        _damage = damage;
    }

    public void Fire()
    {
        _wasFired = true;
    }
}
