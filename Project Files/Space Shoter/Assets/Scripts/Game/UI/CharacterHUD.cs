using UnityEngine;
using UnityEngine.UI;

public class CharacterHUD : MonoBehaviour
{
    [SerializeField] private CharacterEntity _character = null;
    [SerializeField] private Image _playerHealth = null;

    private void Start()
    {
        _character.Health.OnHealthChange += UpdateHealth;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        _playerHealth.fillAmount = _character.Health.CurrentHealth / _character.Health.MaxHealth;
    }

    private void OnDestroy()
    {
        _character.Health.OnHealthChange -= UpdateHealth;
    }
}
