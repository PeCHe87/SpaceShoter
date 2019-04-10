using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static System.Action OnPlayerDead;
    public static System.Action<bool> OnBoostActivated;
    public static System.Action<float> OnBoostAmountChanged;

    [SerializeField] private HealthController _playerHealth = null;

    private void Awake()
    {
        if (_playerHealth != null)
            _playerHealth.OnDead += PlayerDead;
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
            _playerHealth.OnDead -= PlayerDead;
    }

    private void PlayerDead()
    {
        if (OnPlayerDead != null)
            OnPlayerDead();
    }
}
