using Unity.Entities;
[GenerateAuthoringComponent]
public struct EntityToSpawnData : IBufferElementData
{
    public SpawnData Value;
    public static implicit operator EntityToSpawnData(SpawnData e)
    {
        return new EntityToSpawnData { Value = e };
    }
}
