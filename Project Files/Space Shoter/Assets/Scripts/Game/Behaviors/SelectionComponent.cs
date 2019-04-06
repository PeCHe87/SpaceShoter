using UnityEngine;

public class SelectionComponent : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private Canvas _uiIndicator = null;

    private Selectable _currentSelection = null;

    private void Start()
    {
        _uiIndicator.enabled = false;
    }

    private void Update()
    {
        if (_camera == null)
            return;

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                // Checks if it is selectable
                Selectable selectable = hitInfo.transform.GetComponentInParent<Selectable>();

                if (selectable != null)
                {
                    // If it was previously selected then hide the selection UI element.
                    if (selectable.Equals(_currentSelection))
                        HideIndicator();
                    else
                        ShowIndicator(selectable);
                }
                else
                {
                    // If it isn't a selectable object hides the indicator.
                    HideIndicator();
                }
            }
            else
            {
                // Hides selection UI
                HideIndicator();
            }
        }
    }

    private void ShowIndicator(Selectable selectable)
    {
        // Assigns the current selection
        _currentSelection = selectable;

        // Show the indicator above this selected object
        _uiIndicator.transform.position = _currentSelection.Pivot;
        _uiIndicator.enabled = true;
    }

    private void HideIndicator()
    {
        _uiIndicator.enabled = false;
        _currentSelection = null;
    }
}
