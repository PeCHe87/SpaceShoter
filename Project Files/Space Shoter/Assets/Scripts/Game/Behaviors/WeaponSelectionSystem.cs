using UnityEngine;

public class WeaponSelectionSystem : MonoBehaviour
{
    public static System.Action<ScriptableWeapon> OnWeaponSelection;
    public static System.Action<int> OnWeaponIndexSelection;

    [SerializeField] private ScriptableWeapon[] _weapons = null;

    private int _currentWeapon = -1;

    private void Start()
    {
        if (_weapons.Length > 0)
        {
            WeaponSelection(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _currentWeapon != 0)       // Checks for first weapon selection
            WeaponSelection(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2) && _currentWeapon != 1)  // Checks for second weapon selection
            WeaponSelection(1);
    }

    public void WeaponSelection(int index)
    {
        if (index < _weapons.Length)
        {
            // Checks if weapon selected is different from current one
            if (_currentWeapon != index)
            {
                _currentWeapon = index;

                if (OnWeaponSelection != null)
                    OnWeaponSelection(_weapons[index]);

                if(OnWeaponIndexSelection != null)
                    OnWeaponIndexSelection(index);
            }
        }
    }
}
