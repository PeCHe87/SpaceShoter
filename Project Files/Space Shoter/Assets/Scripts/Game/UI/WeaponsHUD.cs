using UnityEngine;
using UnityEngine.UI;

public class WeaponsHUD : MonoBehaviour
{
    [SerializeField] private Image[] _weaponSelections = null;

    private int _weaponsAmount = 0;

    private void Awake()
    {
        WeaponSelectionSystem.OnWeaponIndexSelection += WeaponSelection;

        _weaponsAmount = _weaponSelections.Length;
    }

    private void OnDestroy()
    {
        WeaponSelectionSystem.OnWeaponIndexSelection -= WeaponSelection;
    }

    private void WeaponSelection(int index)
    {
        for (int i = 0; i < _weaponsAmount; i++)
        {
            _weaponSelections[i].enabled = (index == i);
        }
    }
}
