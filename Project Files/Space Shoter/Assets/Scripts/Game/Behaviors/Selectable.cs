using UnityEngine;

public class Selectable : MonoBehaviour
{
    [SerializeField] private Transform _indicatorPivot = null;

    public Vector3 Pivot
    {
        get
        {
            return _indicatorPivot.TransformPoint(_indicatorPivot.localPosition);
        }
    }
}
