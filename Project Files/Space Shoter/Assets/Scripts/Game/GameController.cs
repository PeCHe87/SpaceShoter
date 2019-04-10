using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ScriptableGameConfiguration _configuration = null;

    public static ScriptableGameConfiguration Configuration = null;

    private void Awake()
    {
        Configuration = _configuration;
    }
}