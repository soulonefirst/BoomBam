using Unity.Entities;
public struct PrefabsData : IBufferElementData
{
    public Entity Value;
    public static implicit operator PrefabsData(Entity e)
    {
        return new PrefabsData { Value = e };
    }
}
