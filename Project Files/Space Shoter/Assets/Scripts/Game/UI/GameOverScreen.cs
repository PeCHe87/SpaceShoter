using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private HealthController _playerHealth = null;

    private Canvas _canvas = null;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        if (_playerHealth != null)
            _playerHealth.OnDead += PlayerDead;

        GameEventsManager.OnFuelEmpty += PlayerDead;
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
            _playerHealth.OnDead -= PlayerDead;

        GameEventsManager.OnFuelEmpty -= PlayerDead;
    }

    private void PlayerDead()
    {
        if (_canvas == null)
            return;

        _canvas.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
