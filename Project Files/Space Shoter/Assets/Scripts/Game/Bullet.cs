using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool _wasFired = false;
    private float _speed = 1;
    private float _lifeTime = 5;

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

        Destroy(gameObject);
    }

    public void Fire(float speedMovement, float lifetime)
    {
        _speed = speedMovement;
        _lifeTime = lifetime;
        _wasFired = true;
    }
}
