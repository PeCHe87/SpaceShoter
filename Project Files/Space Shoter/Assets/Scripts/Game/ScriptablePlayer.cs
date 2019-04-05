using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "GameElements/Player", order = 2)]
[System.Serializable]
public class ScriptablePlayer : ScriptableObject
{
    public float MaxHealth;
}