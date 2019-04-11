using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject _graphics = null;
    [SerializeField] private float _rotationSpeed = 1;

    private bool _collected = false;

    private void Update()
    {
        if (_collected)
            return;

        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_collected)
            return;

        Player player = other.GetComponent<Player>();

        if (player != null)
        {
            _collected = true;

            // TODO: show FXs

            // Hide object
            HideGraphics();

            // Communicates to the game the collectible was collected
            OnCollected();
        }
    }

    private void OnCollected()
    {
        if (GameEventsManager.OnCollectableTaken != null)
            GameEventsManager.OnCollectableTaken();
    }

    private void HideGraphics()
    {
        _graphics.SetActive(false);

        Destroy(gameObject, 3);
    }
}
