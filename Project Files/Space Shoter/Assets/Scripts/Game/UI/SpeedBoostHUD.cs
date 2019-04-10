using UnityEngine;
using UnityEngine.UI;

public class SpeedBoostHUD : MonoBehaviour
{
    [SerializeField] private Image _progressFill = null;
    [SerializeField] private Color _activeColor = Color.white;
    [SerializeField] private Color _deactiveColor = Color.white;

    private void Awake()
    {
        GameEventsManager.OnBoostAmountChanged += AmountChanged;
        GameEventsManager.OnBoostActivated += Activated;
    }

    private void OnDestroy()
    {
        GameEventsManager.OnBoostAmountChanged -= AmountChanged;
        GameEventsManager.OnBoostActivated -= Activated;
    }

    private void Activated(bool active)
    {
        _progressFill.color = (active) ? _activeColor : _deactiveColor;
    }

    private void AmountChanged(float currentProgress)
    {
        if (_progressFill == null)
            return;

        _progressFill.fillAmount = currentProgress;
    }
}
