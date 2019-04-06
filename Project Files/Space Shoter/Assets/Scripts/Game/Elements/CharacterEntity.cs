using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    [SerializeField] private GameObject _graphics = null;

    private HealthController _health = null;

    public HealthController Health
    {
        get
        {
            return _health;
        }

        protected set
        {
            _health = value;
        }
    }

    public void HideGraphics()
    {
        _graphics.SetActive(false);
    }

    public void ShowGraphics()
    {
        _graphics.SetActive(true);
    }
}
