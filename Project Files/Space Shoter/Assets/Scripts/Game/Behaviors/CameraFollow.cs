using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _forwardOffset = 0;
    [SerializeField] private float _heightOffset = 0;
    [SerializeField] private float _speed = 0;
    [SerializeField] private float _rotationSpeed = 0;
    [SerializeField] private bool _canRotate = false;

    private Vector3 _targetPosition = Vector3.zero;

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (_target != null)
        {
            _targetPosition.x = _target.position.x + _forwardOffset;
            _targetPosition.y = _target.position.y + _heightOffset;
            _targetPosition.z = _target.position.z + _forwardOffset;

            transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);

            if (_canRotate)
            {
                var _targetRotation = Quaternion.LookRotation(_target.position - transform.position, _target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _rotationSpeed);
            }
        }
    }
}
