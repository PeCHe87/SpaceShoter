using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ScriptableWeapon _weaponData = null;
    [SerializeField] private Transform _aim = null;

    private float _currentRate = 0;
    private bool _canFire = true;

    private void Update()
    {
        if (_currentRate > 0)
        {
            _currentRate -= Time.deltaTime;

            if (_currentRate <= 0)
                _canFire = true;
        }
    }

    [ContextMenu("Fire")]
    public void Fire()
    {
        if (!_canFire)
            return;

        // TODO: use a pool to avoid instantiate each bullet
        Bullet bullet = Instantiate<Bullet>(_weaponData.Bullet, _aim.position, _aim.rotation);

        // Setup bullet and fire it
        bullet.Setup(_weaponData.BulletSpeedMovement, _weaponData.BulletLifetime, _weaponData.BulletDamage);
        bullet.Fire();

        _currentRate = _weaponData.FireRate;
        _canFire = false;
    }
}
