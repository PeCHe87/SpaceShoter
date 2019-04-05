using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Player _player = null;
    [SerializeField] private Image _playerHealth = null;

    private void Start()
    {
        _player.Health.OnHealthChange += UpdateHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        _playerHealth.fillAmount = _player.Health.CurrentHealth / _player.Health.MaxHealth;
        Debug.Log(_player.Health.CurrentHealth);
    }

    private void OnDestroy()
    {
        _player.Health.OnHealthChange -= UpdateHealth;
    }
}
