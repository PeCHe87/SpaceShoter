using UnityEngine;

public class FuelConsumptionComponent : MonoBehaviour
{
    // Properties will be set from Configuration
    private float _consumingTimeStep = 0;
    private float _maxAmount = 0;
    private ScriptableGameConfiguration _configuration = null;

    // Internal data about the component
    private bool _isConsuming = false;
    private bool _enabled = false;
    private float _currentAmount = 0;
    private bool _isCharging = false;

    public bool Enabled
    {
        set { _enabled = value; }
    }

    public bool Consuming
    {
        set { _isConsuming = value; }
    }

    public bool Charging
    {
        set { _isCharging = value; }
    }

    private void Start()
    {
        _configuration = GameController.Configuration;

        Setup(_configuration.FuelConsumeTimeStep, _configuration.FuelMaxCharge);
    }

    private void Setup(float consumingStep, float maxAmount)
    {
        _consumingTimeStep = consumingStep;
        _maxAmount = maxAmount;

        _currentAmount = _maxAmount;

        _isConsuming = false;

        _isCharging = false;

        if (GameEventsManager.OnFuelAmountChanged != null)
            GameEventsManager.OnFuelAmountChanged(_currentAmount / _maxAmount);

        _enabled = true;
    }

    private void Update()
    {
        if (!_enabled)
            return;

        if (_currentAmount <= 0)
            return;

        if (_isConsuming)
            Consume();
    }

    private void Consume()
    {
        _currentAmount -= _consumingTimeStep * Time.deltaTime;

        if (GameEventsManager.OnFuelAmountChanged != null)
            GameEventsManager.OnFuelAmountChanged(_currentAmount / _maxAmount);

        if (_currentAmount <= 0)
        {
            if (GameEventsManager.OnFuelEmpty != null)
                GameEventsManager.OnFuelEmpty();
        }
    }

    public bool Charge(float amount)
    {
        if (_currentAmount >= _maxAmount)
            return false;

        //_currentAmount = Mathf.Clamp(_currentAmount + _chargingTimeStep * Time.deltaTime, 0, _maxAmount);
        _currentAmount = Mathf.Clamp(_currentAmount + amount, 0, _maxAmount);

        if (GameEventsManager.OnFuelAmountChanged != null)
            GameEventsManager.OnFuelAmountChanged(_currentAmount / _maxAmount);

        return (_currentAmount < _maxAmount);
    }
}
