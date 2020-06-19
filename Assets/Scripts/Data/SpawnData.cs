using Unity.Entities;
using UnityEngine;
[GenerateAuthoringComponent]
public struct SpawnData : IComponentData
{
    public int numEntityToSpawn;
    public MoveData moveData;
    public DamageData damageData;
    public bool alreadySpawn;
    public Color color;
}