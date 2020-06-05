using Unity.Entities;
[GenerateAuthoringComponent]
public struct TakeDamageData :IComponentData
{
    public DamageData Value;
    public Entity previousSorce;
}
