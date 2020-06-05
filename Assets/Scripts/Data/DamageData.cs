using Unity.Entities;
[GenerateAuthoringComponent]
public struct DamageData : IComponentData
{
    public int damage;
    public Entity sorceEntity;
}
