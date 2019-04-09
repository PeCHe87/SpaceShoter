using UnityEngine;

public class PlayerMovementByPhysics : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 1;
    [SerializeField] private float _speedMovement = 1;
    [SerializeField] private float _speedMovementReverse = 1;
    [SerializeField] private float _speedSmoothBreak = 1;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private Transform _target = null;

    private Quaternion _targetRotation = Quaternion.identity;
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        // Input detection
        float forward = Input.GetAxisRaw("Vertical");
        float lateral = Input.GetAxisRaw("Horizontal");

        Vector3 input = new Vector3(lateral, 0, forward);

        RotationMovement(lateral);

        // Movement
        if (Mathf.Abs(forward) > 0)
        {
            _velocity = _target.forward * forward * Time.deltaTime;
            _velocity *= (forward > 0) ? _speedMovement : _speedMovementReverse;
        }
        else if (Mathf.Abs(_rigidbody.velocity.z) > 0)
        {
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, _speedSmoothBreak * Time.deltaTime);
        }

        _rigidbody.velocity = _velocity;
    }

    private void RotationMovement(float lateral)
    {
        // Rotation movement
        Vector3 rot = new Vector3(0, lateral, 0);
        rot = rot.normalized * _speedRotation * Time.deltaTime;
        _target.Rotate(rot, Space.World);
    }
}
