using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private ScriptableWeapon _weaponData = null;
    [SerializeField] private Transform _aim = null;

    private float _currentRate = 0;
    private bool _canFire = true;
    private Transform _targetSelected = null;

    private void Awake()
    {
        SelectionComponent.OnSelected += SelectTarget;
        SelectionComponent.OnDeselected += DeselectTarget;
    }

    private void OnDestroy()
    {
        SelectionComponent.OnSelected -= SelectTarget;
        SelectionComponent.OnDeselected -= DeselectTarget;
    }

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

        // Check if the bullet is missile then if there is any target selected
        if (_weaponData.Type.Equals(Bullet.BulletType.HOMING_MISSILE) && _targetSelected == null)
        {
            // TODO: show a UI message to advice the player about select an enemy before firing a missile
            Debug.Log("NO TARGET SELECTED");
            return;
        }

        // TODO: use a pool to avoid instantiate each bullet
        Bullet bullet = Instantiate<Bullet>(_weaponData.Bullet, _aim.position, _aim.rotation);

        // Setup bullet and fire it
        bullet.Setup(_weaponData.BulletSpeedMovement, _weaponData.BulletLifetime, _weaponData.BulletDamage, _weaponData.Type);

        // Check if it has to assign the target
        if (bullet.Type.Equals(Bullet.BulletType.HOMING_MISSILE))
            bullet.SetTarget(_targetSelected.transform);

        bullet.Fire();

        _currentRate = _weaponData.FireRate;
        _canFire = false;
    }

    private void DeselectTarget()
    {
        _targetSelected = null;
    }

    private void SelectTarget(Transform target)
    {
        Enemy enemy = target.GetComponent<Enemy>();

        if (enemy != null)
            _targetSelected = target;
        else
            _targetSelected = null;
    }
}
