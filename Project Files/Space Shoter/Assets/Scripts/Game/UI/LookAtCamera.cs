using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera = null;

    private void LateUpdate()
    {
        if (_camera == null)
            return;

        transform.LookAt(_camera, _camera.up);
    }
}
