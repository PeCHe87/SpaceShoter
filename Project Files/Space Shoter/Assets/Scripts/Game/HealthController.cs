using UnityEngine;

public class HealthController : MonoBehaviour
{
    public System.Action OnHealthChange;
    public System.Action OnDead;

    [SerializeField] private float _maxHealth = 1;

    private float _currentHealth = 0;
    private bool _isDead = false;

    public float CurrentHealth
    {
        get
        {
            return _currentHealth;
        }
    }

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void SetMaxHealth(float health)
    {
        _maxHealth = health;
        _currentHealth = health;
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        // Notify with an event the current health has changed
        if (OnHealthChange != null)
            OnHealthChange();

        // Checks if it is dead
        if (_currentHealth <= 0)
        {
            Dead();
        }
    }

    public void AddHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

        // Notify with an event the current health has changed
        if (OnHealthChange != null)
            OnHealthChange();
    }

    private void Dead()
    {
        _isDead = true;

        // Notify with an event the Dead event
        if (OnDead != null)
            OnDead();
    }

    [ContextMenu("TAKE DAMAGE")]
    public void DebugTakeDamage()
    {
        TakeDamage(10);
    }

    [ContextMenu("ADD HEALTH")]
    public void DebugAddHealth()
    {
        AddHealth(5);
    }
}