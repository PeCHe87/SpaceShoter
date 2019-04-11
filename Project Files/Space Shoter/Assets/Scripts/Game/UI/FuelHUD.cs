using UnityEngine;
using UnityEngine.UI;

public class FuelHUD : MonoBehaviour
{
    [SerializeField] private Image _progressFill = null;

    private void Awake()
    {
        GameEventsManager.OnFuelAmountChanged += AmountChanged;
    }

    private void OnDestroy()
    {
        GameEventsManager.OnFuelAmountChanged -= AmountChanged;
    }

    private void AmountChanged(float currentProgress)
    {
        if (_progressFill == null)
            return;

        _progressFill.fillAmount = currentProgress;
    }
}
