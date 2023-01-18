using UnityEngine;

[CreateAssetMenu(menuName = "Pewpew")]
public class ShootingData : ScriptableObject
{
    public GameObject Shot;

    public float FireRate = 2;
    public float BulletSpeed = 10;
}