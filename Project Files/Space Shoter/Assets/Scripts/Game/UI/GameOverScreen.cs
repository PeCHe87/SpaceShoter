using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private HealthController _playerHealth = null;
    [SerializeField] private TextMeshProUGUI _txtMessage = null;

    private Canvas _canvas = null;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        if (_playerHealth != null)
            _playerHealth.OnDead += PlayerDead;

        GameEventsManager.OnFuelEmpty += PlayerDeadByEmptyFuel;

        GameEventsManager.OnAllCollectablesWereTaken += PlayerWins;
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
            _playerHealth.OnDead -= PlayerDead;

        GameEventsManager.OnFuelEmpty -= PlayerDeadByEmptyFuel;

        GameEventsManager.OnAllCollectablesWereTaken -= PlayerWins;
    }

    private void PlayerDead()
    {
        ShowGameOver("GAME OVER - YOU WERE DESTROYED", Color.red);
    }

    private void PlayerDeadByEmptyFuel()
    {
        ShowGameOver("GAME OVER - RUN OUT OF FUEL", Color.red);
    }

    private void PlayerWins()
    {
        ShowGameOver("GAME WINS!", Color.green);
    }

    private void ShowGameOver(string message, Color color)
    {
        if (_canvas == null)
            return;

        _txtMessage.text = message;
        _txtMessage.color = color;

        _canvas.enabled = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Main");
    }
}
