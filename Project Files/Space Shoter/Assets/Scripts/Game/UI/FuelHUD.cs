using UnityEngine;
using UnityEngine.UI;

public class FuelHUD : MonoBehaviour
{
    [SerializeField] private Image _progressFill = null;
    [SerializeField] private Color _regularColor = Color.white;
    [SerializeField] private Color _chargingColor = Color.white;

    private void Awake()
    {
        GameEventsManager.OnFuelAmountChanged += AmountChanged;
        GameEventsManager.OnFuelStartCharging += StartCharging;
        GameEventsManager.OnFuelStopCharging += StopCharging;
    }

    private void OnDestroy()
    {
        GameEventsManager.OnFuelAmountChanged -= AmountChanged;
        GameEventsManager.OnFuelStartCharging -= StartCharging;
        GameEventsManager.OnFuelStopCharging -= StopCharging;
    }

    private void StartCharging()
    {
        _progressFill.color = _chargingColor;
    }

    private void StopCharging()
    {
        _progressFill.color = _regularColor;
    }

    private void AmountChanged(float currentProgress)
    {
        if (_progressFill == null)
            return;

        _progressFill.fillAmount = currentProgress;
    }
}
