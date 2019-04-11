using System;
using TMPro;
using UnityEngine;

public class CollectablesHUD : MonoBehaviour
{
    [SerializeField] private Collectable[] _collectables = null;
    [SerializeField] private TextMeshProUGUI _txtCollectables = null;

    private int _amountCollected = 0;

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
    }

    private void UpdateLabel()
    {
        _txtCollectables.text = _amountCollected.ToString() + "/" + _collectables.Length.ToString();
    }
}
