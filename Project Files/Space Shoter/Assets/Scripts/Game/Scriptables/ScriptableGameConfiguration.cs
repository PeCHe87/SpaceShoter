using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Game Elements/Configuration", order = 4)]
[System.Serializable]
public class ScriptableGameConfiguration : ScriptableObject
{
    public LayerMask PlayerLayerMask;

    [Header("Boost properties")]
    public float BoostConsumeTimeStep;
    public float BoostMaxCharge;
    public float BoostReloadingTimeStep;

    [Header("Fuel properties")]
    public float FuelConsumeTimeStep;
    public float FuelMaxCharge;
}