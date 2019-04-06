using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Game Elements/Player", order = 2)]
[System.Serializable]
public class ScriptablePlayer : ScriptableObject
{
    public float MaxHealth;
}