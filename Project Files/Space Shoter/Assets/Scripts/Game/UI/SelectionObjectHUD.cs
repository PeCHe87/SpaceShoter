using TMPro;
using UnityEngine;

public class SelectionObjectHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtObjectSelectedName = null;
    [SerializeField] private TextMeshProUGUI _txtObjectSelectedHealth = null;

    private HealthController _health = null;

    private void Awake()
    {
        ObjectDeselection();

        SelectionComponent.OnSelected += ObjectSelection;
        SelectionComponent.OnDeselected += ObjectDeselection;
    }

    private void ObjectDeselection()
    {
        _txtObjectSelectedName.enabled = false;
        _txtObjectSelectedHealth.enabled = false;

        // Unsuscribes previous event if it exists
        UnsuscribeHealthEvent();
    }

    private void ObjectSelection(Transform obj)
    {
        _txtObjectSelectedName.text = obj.name;
        _txtObjectSelectedName.enabled = true;

        // Unsuscribes previous event if it exists
        UnsuscribeHealthEvent();

        _health = obj.GetComponent<HealthController>();

        if (_health != null)
        {
            _txtObjectSelectedHealth.text = _health.CurrentHealth + " / " + _health.MaxHealth;
            _txtObjectSelectedHealth.enabled = true;

            // Subscribes to the health changed events
            _health.OnHealthChange += UpdateObjectSelectedHealth;
        }
        else
        {
            _txtObjectSelectedHealth.enabled = false;
        }
    }

    private void UnsuscribeHealthEvent()
    {
        if (_health != null && _health.OnHealthChange != null)
            _health.OnHealthChange -= UpdateObjectSelectedHealth;
    }

    private void UpdateObjectSelectedHealth()
    {
        if (_health.CurrentHealth > 0)
            _txtObjectSelectedHealth.text = _health.CurrentHealth + " / " + _health.MaxHealth;
        else
            _txtObjectSelectedHealth.text = "DEAD";
    }

    private void OnDestroy()
    {
        SelectionComponent.OnSelected -= ObjectSelection;
        SelectionComponent.OnDeselected -= ObjectDeselection;

        // Unsuscribes previous event if it exists
        UnsuscribeHealthEvent();
    }


}
