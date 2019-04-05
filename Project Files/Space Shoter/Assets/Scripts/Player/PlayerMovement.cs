using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement = 1;
    [SerializeField] private float _speedRotation = 1;
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _drawRayLength = 20;

    private Vector3 _destination = Vector3.zero;
    private float distance = 0;
    private Vector3 offset = Vector3.zero;

    private void Update()
    {
        // Input detection
        float forward = Input.GetAxisRaw("Vertical");
        float lateral = Input.GetAxisRaw("Horizontal");

        RotationMovement(lateral);

        ForwardMovement(forward);

        Debug.DrawRay(_target.localPosition, _target.forward * _drawRayLength, Color.green);
    }

    private void RotationMovement(float lateral)
    {
        // Rotation movement
        Vector3 rot = new Vector3(0, lateral, 0);
        rot = rot.normalized * _speedRotation * Time.deltaTime;
        _target.Rotate(rot, Space.World);
    }

    private void ForwardMovement(float forward)
    {
        // If there is some input for forward movement
        if (Mathf.Abs(forward) > 0)
        {
            _destination = _target.localPosition + _target.forward * forward;
            offset = _destination - _target.localPosition;
            distance = offset.sqrMagnitude;
        }

        // Checks distance from last movement
        if (distance > 0)
        {
            _target.localPosition = Vector3.Lerp(_target.localPosition, _destination, _speedMovement * Time.deltaTime);
        }
    }
}
