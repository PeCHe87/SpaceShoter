using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static System.Action OnPlayerDead;
    public static System.Action<bool> OnBoostActivated;
    public static System.Action<float> OnBoostAmountChanged;
    public static System.Action<float> OnFuelAmountChanged;
    public static System.Action OnFuelEmpty;
    public static System.Action OnFuelStartCharging;
    public static System.Action OnFuelStopCharging;
    public static System.Action OnCollectableTaken;
    public static System.Action OnAllCollectablesWereTaken;
    public static System.Action OnCameraShake;

    [SerializeField] private HealthController _playerHealth = null;

    private void Awake()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnDead += PlayerDead;
            _playerHealth.OnTakesDamage += PlayerTakesDamage;
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnDead -= PlayerDead;
            _playerHealth.OnTakesDamage += PlayerTakesDamage;
        }
    }

    private void PlayerDead()
    {
        if (OnPlayerDead != null)
            OnPlayerDead();
    }

    private void PlayerTakesDamage()
    {
        if (OnCameraShake != null)
            OnCameraShake();
    }
}
