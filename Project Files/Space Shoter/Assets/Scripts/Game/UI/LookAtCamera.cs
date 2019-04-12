using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera = null;

    private void LateUpdate()
    {
        if (_camera == null)
            return;

        Quaternion lookAt = Quaternion.LookRotation(_camera.forward, _camera.up);
        transform.rotation = lookAt;
    }
}
