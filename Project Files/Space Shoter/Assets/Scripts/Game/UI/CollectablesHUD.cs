using TMPro;
using UnityEngine;

public class CollectablesHUD : MonoBehaviour
{
    [SerializeField] private Collectable[] _collectables = null;
    [SerializeField] private TextMeshProUGUI _txtCollectables = null;

    private int _amountCollected = 0;
    private int _collectablesAmount = 0;

    private void Awake()
    {
        GameEventsManager.OnCollectableTaken += CollectableWasTaken;
    }

    private void Start()
    {
        _amountCollected = 0;
        UpdateLabel();
    }

    private void OnDestroy()
    {
        GameEventsManager.OnCollectableTaken -= CollectableWasTaken;
    }

    private void CollectableWasTaken()
    {
        _amountCollected++;

        UpdateLabel();

        // TODO: move this logic to a Game Manager
        if (_amountCollected == _collectables.Length)
        {
            if (GameEventsManager.OnAllCollectablesWereTaken != null)
                GameEventsManager.OnAllCollectablesWereTaken();
        }
    }

    private void UpdateLabel()
    {
        _txtCollectables.text = _amountCollected.ToString() + "/" + _collectables.Length.ToString();
    }
}
