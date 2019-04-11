using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Game Elements/Fuel Charge Station", order = 5)]
[System.Serializable]
public class ScriptableFuelChargeStation : ScriptableObject
{
    public float ChargingTimeStep;
    public float MaxCharge;
}