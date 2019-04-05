using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ScriptablePlayer _playerData = null;

    private HealthController _health = null;

    public HealthController Health
    {
        get
        {
            return _health;
        }
    }

    private void Awake()
    {
        _health = GetComponent<HealthController>();
        _health.SetMaxHealth(_playerData.MaxHealth);
    }
}
