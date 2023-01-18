using UnityEngine;

[CreateAssetMenu(menuName = "Astriod/Spawner Data")]
public class SpawnerData : ScriptableObject
{
    //[Header("Spawning stats")]
    public float SpawnRate = 1;
    public float MinSize = 0.2f;
    public float MaxSize = 0.8f;

    //[Header("Launching")]
    public float MinForce = 1;
    public float MaxForce = 2;
    public float Angle = 10;

    //[Header("Scaling (10s)")]
    public float RateIncrease = 0.2f;
    public float ForceIncrease = 0.15f;
}
