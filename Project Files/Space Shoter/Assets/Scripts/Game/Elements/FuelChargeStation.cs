using UnityEngine;

public class FuelChargeStation : MonoBehaviour
{
    [SerializeField] private ScriptableFuelChargeStation _data = null;

    // Properties from Configuration
    private LayerMask _playerLayerMask = 0;
    private float _chargingTimeStep = 0;
    private float _maxCharge = 0;

    private float _currentCharge = 0;
    private bool _playerCharging = false;
    private BoxCollider _collider = null;
    private FuelConsumptionComponent _fuelComponent = null;

    private void Start()
    {
        _playerLayerMask = GameController.Configuration.PlayerLayerMask;
        _chargingTimeStep = _data.ChargingTimeStep;
        _maxCharge = _data.MaxCharge;

        _currentCharge = _maxCharge;

        _collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _fuelComponent = other.GetComponent<FuelConsumptionComponent>();

        if (_fuelComponent != null)
        {
            _playerCharging = true;

            if (GameEventsManager.OnFuelStartCharging != null)
                GameEventsManager.OnFuelStartCharging();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _fuelComponent = other.GetComponent<FuelConsumptionComponent>();

        if (_fuelComponent != null)
        {
            _fuelComponent = null;

            _playerCharging = false;

            if (GameEventsManager.OnFuelStopCharging != null)
                GameEventsManager.OnFuelStopCharging();
        }
    }

    private void Update()
    {
        if (!_playerCharging)
            return;

        if (_currentCharge <= 0)
            return;

        if (_fuelComponent == null)
            return;

        float amount = _chargingTimeStep * Time.deltaTime;

        _currentCharge -= amount;

        bool canContinueCharging = _fuelComponent.Charge(amount);

        // If fuel is full then stop charging
        if (!canContinueCharging)
        {
            _playerCharging = false;

            if (GameEventsManager.OnFuelStopCharging != null)
                GameEventsManager.OnFuelStopCharging();
        }

        // Checks if charge station has charge
        if (_currentCharge <= 0)
            Empty();
    }

    private void Empty()
    {
        _collider.enabled = false;
        _playerCharging = false;
        _fuelComponent = null;

        if (GameEventsManager.OnFuelStopCharging != null)
            GameEventsManager.OnFuelStopCharging();
    }
}
