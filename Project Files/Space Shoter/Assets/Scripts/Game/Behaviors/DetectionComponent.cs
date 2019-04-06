using UnityEngine;

public class DetectionComponent : MonoBehaviour
{
    public System.Action<Transform> OnDetectedTarget;
    public System.Action OnDetectedTargetLose;

    [SerializeField] private bool _canDebug = true;

    private LayerMask _maskDetection = 0;
    private float _radius = 0;
    private float _maxDistance = 0;
    private bool _canDetect = false;
    private Transform _target = null;

    public void Setup(LayerMask layer, float radius)
    {
        _maskDetection = layer;
        _radius = radius;

        _canDetect = true;
    }

    private void Update()
    {
        if (!_canDetect)
            return;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, transform.up, _maxDistance, _maskDetection);

        if (hits.Length > 0)
        {
            Transform hitTarget = hits[0].transform.parent;

            if (!hitTarget.Equals(_target))
            {
                _target = hitTarget;

                if (OnDetectedTarget != null)
                    OnDetectedTarget(_target);
            }
        }
        else
        {
            _target = null;

            if (OnDetectedTargetLose != null)
                OnDetectedTargetLose();
        }
    }

    void OnDrawGizmos()
    {
        if (!_canDebug)
            return;

        // Draw a yellow sphere at the transform's position
        Color gizmoColor = Color.green;
        gizmoColor.a = 0.1f;
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
