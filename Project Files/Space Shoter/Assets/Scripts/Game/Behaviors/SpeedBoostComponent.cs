using UnityEngine;

public class SpeedBoostComponent : MonoBehaviour
{
    // Properties will be set from Configuration
    private float _reloadingTimeStep = 0;
    private float _consumingTimeStep = 0;
    private float _maxAmount = 0;
    private ScriptableGameConfiguration _configuration = null;

    // Internal data about the component
    private bool _isActive = false;
    private bool _enabled = false;
    private float _currentAmount = 0;
    private bool _fullReloading = false;

    public bool Enable
    {
        set { _enabled = value; }
    }

    private void Start()
    {
        _configuration = GameController.Configuration;

        Setup(_configuration.BoostReloadingTimeStep, _configuration.BoostConsumeTimeStep, _configuration.BoostMaxCharge);
    }

    private void Setup(float reloadingStep, float consumingStep, float maxAmount)
    {
        _fullReloading = true;

        _reloadingTimeStep = reloadingStep;
        _consumingTimeStep = consumingStep;
        _maxAmount = maxAmount;

        _currentAmount = 0;

        _isActive = false;

        if (GameEventsManager.OnBoostAmountChanged != null)
            GameEventsManager.OnBoostAmountChanged(_currentAmount / _maxAmount);

        if (GameEventsManager.OnBoostActivated != null)
            GameEventsManager.OnBoostActivated(false);

        _enabled = true;
    }

    private void Update()
    {
        if (!_enabled)
            return;

        if (_fullReloading)
        {
            Reload();
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isActive = !_isActive;

            if (GameEventsManager.OnBoostActivated != null)
                GameEventsManager.OnBoostActivated(_isActive);
        }

        // Consuming if it is active
        if (_isActive)
            Consume();
        else
            Reload();
    }

    private void Consume()
    {
        _currentAmount -= _consumingTimeStep * Time.deltaTime;

        if (_currentAmount <= 0)
        {
            _currentAmount = 0;

            // Deactivates automatically until full reloading
            _isActive = false;

            // Starts full reloading
            _fullReloading = true;

            // Informs that boost speed was deactivated
            if (GameEventsManager.OnBoostActivated != null)
                GameEventsManager.OnBoostActivated(false);
        }

        if (GameEventsManager.OnBoostAmountChanged != null)
            GameEventsManager.OnBoostAmountChanged(_currentAmount/_maxAmount);
    }

    private void Reload()
    {
        if (_currentAmount >= _maxAmount)
            return;

        _currentAmount += _consumingTimeStep * Time.deltaTime;

        if (_currentAmount >= _maxAmount)
        {
            _currentAmount = _maxAmount;
            _fullReloading = false;
        }

        if (GameEventsManager.OnBoostAmountChanged != null)
            GameEventsManager.OnBoostAmountChanged(_currentAmount / _maxAmount);
    }
}