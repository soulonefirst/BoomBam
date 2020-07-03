using Unity.Entities;
using UnityEngine;
public struct SpawnData : IComponentData
{
    public int numPrefabToSpawn;
    public MoveData moveData;
    public DamageData damageData;
    public Color color;
}