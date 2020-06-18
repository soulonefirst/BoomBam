using Unity.Entities;
[GenerateAuthoringComponent]
public struct SpawnData : IComponentData
{
    public Entity entityToSpawn;
    public MoveData moveData;
    public DamageData damageData;
    public bool alreadySpawn;
}