using UnityEngine;

public class HomingMissile : Bullet
{
    private Rigidbody _rigidbody = null;
    private Quaternion targetRotation = Quaternion.identity;
    private float _maxDegreesDelta = 10;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();   
    }

    private void FixedUpdate()
    {
        if (!_wasFired)
            return;

        if (_target == null)
            return;

        _lifeTime -= Time.deltaTime;

        if (_lifeTime <= 0)
            Destroy(gameObject);
        else
            FollowTarget();
    }

    private void FollowTarget()
    {
        _rigidbody.velocity = transform.forward * _speed * Time.fixedDeltaTime;

        targetRotation = Quaternion.LookRotation(_target.position - transform.position);

        _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, _maxDegreesDelta));
    }
}
