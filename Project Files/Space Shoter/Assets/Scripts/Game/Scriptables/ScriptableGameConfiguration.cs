using UnityEngine;

[CreateAssetMenu(fileName = "GameConfiguration", menuName = "Game Elements/Configuration", order = 4)]
[System.Serializable]
public class ScriptableGameConfiguration : ScriptableObject
{
    public float BoostConsumeTimeStep;
    public float BoostMaxCharge;
    public float BoostReloadingTimeStep;
}