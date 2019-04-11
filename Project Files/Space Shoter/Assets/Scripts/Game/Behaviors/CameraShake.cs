using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float _duration = 0f;
    [SerializeField] private float _amount = 0.7f;
    [SerializeField] private float _decreasingTimeStep = 1.0f;

    private bool _isShaking = false;
    private Vector3 _originalPosition = Vector3.zero;
    private float _currentDuration = 0;

    private void Awake()
    {
        _originalPosition = transform.localPosition;

        // Subscription to invoke shake
        GameEventsManager.OnCameraShake += StartShake;
    }

    private void OnDestroy()
    {
        GameEventsManager.OnCameraShake -= StartShake;
    }

    private void LateUpdate()
    {
        if (!_isShaking)
            return;

        if (_currentDuration > 0)
        {
            float perc = _currentDuration / _duration;
            transform.localPosition = Vector3.Lerp(transform.localPosition, _originalPosition + Random.insideUnitSphere * _amount, perc);

            _currentDuration -= Time.deltaTime * _decreasingTimeStep;

            if (_currentDuration <= 0)
                StopShake();
        }
    }

    private void StopShake()
    {
        _currentDuration = 0;
        _isShaking = false;
    }

    [ContextMenu("Start Shake")]
    private void StartShake()
    {
        _originalPosition = transform.localPosition;
        _currentDuration = _duration;
        _isShaking = true;
    }
}