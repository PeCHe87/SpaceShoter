using System;
using UnityEngine;

public class PlayerMovementByPhysics : MonoBehaviour
{
    [SerializeField] private float _speedRotation = 1;
    [SerializeField] private float _smoothInput = 0.5f;
    [SerializeField] private float _speedMovement = 1;
    [SerializeField] private float _speedMovementReverse = 1;
    [SerializeField] private float _speedSmoothBreak = 1;
    [SerializeField] private Rigidbody _rigidbody = null;
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _recoveryTimeAfterColliding = 0;

    private Quaternion _targetRotation = Quaternion.identity;
    private Vector3 _velocity = Vector3.zero;
    private PushBackComponent _collisionDetection = null;
    private float _currentRecoveryTimeAfterColliding = 0;
    private bool _enabled = true;

    private void Awake()
    {
        _collisionDetection = GetComponent<PushBackComponent>();

        if (_collisionDetection != null)
            _collisionDetection.OnCollision += CollisionDetected;
    }

    private void Update()
    {
        if (!_enabled)
            return;

        if (Colliding())
            return;

        // TODO: change this input detection to a separated InputController class
        // Input detection
        float forward = Input.GetAxisRaw("Vertical");
        float lateral = Input.GetAxisRaw("Horizontal");

        RotationMovement(lateral);

        // Movement
        if (Mathf.Abs(forward) > 0)
        {
            Vector3 _targetVelocity = _target.forward * forward * Time.deltaTime;
            _targetVelocity *= (forward > 0) ? _speedMovement : _speedMovementReverse;
            _velocity = Vector3.Lerp(_velocity, _targetVelocity, (forward > 0) ? _speedMovement : _speedMovementReverse * Time.deltaTime);
        }
        else if (Mathf.Abs(_velocity.x) > 0.1f || Mathf.Abs(_velocity.z) > 0.1f)
        {
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, _speedSmoothBreak * Time.deltaTime);
        }

        _rigidbody.velocity = _velocity;
    }

    internal void Disabled()
    {
        _enabled = false;

        _rigidbody.isKinematic = true;  // Stops physics movement at all
    }

    internal void Enabled()
    {
        _rigidbody.isKinematic = false;  // Enables physics movement

        _enabled = true;
    }

    private void RotationMovement(float lateral)
    {
        // Rotation movement
        Vector3 rot = new Vector3(0, lateral * _smoothInput, 0);
        rot = rot.normalized * _speedRotation * Time.deltaTime;
        _target.Rotate(rot, Space.World);
    }

    private bool Colliding()
    {
        if (_currentRecoveryTimeAfterColliding <= 0)
            return false;

        _currentRecoveryTimeAfterColliding = Mathf.Clamp(_currentRecoveryTimeAfterColliding - Time.deltaTime, 0, _recoveryTimeAfterColliding);

        // Collision recovery time finished so it has to be stopped
        if (_currentRecoveryTimeAfterColliding <= 0)
            _velocity = Vector3.zero;

        return _currentRecoveryTimeAfterColliding > 0;
    }

    private void CollisionDetected()
    {
        Debug.Log("Collision!");
        _currentRecoveryTimeAfterColliding = _recoveryTimeAfterColliding;
    }

    private void OnDestroy()
    {
        if (_collisionDetection != null)
            _collisionDetection.OnCollision -= CollisionDetected;
    }
}
