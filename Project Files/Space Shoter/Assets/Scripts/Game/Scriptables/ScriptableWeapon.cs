using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Game Elements/Weapon", order = 1)]
[System.Serializable]
public class ScriptableWeapon : ScriptableObject
{
    public float BulletSpeedMovement;
    public Bullet Bullet;
    public float FireRate;
    public float BulletLifetime;
    public float BulletDamage;
}