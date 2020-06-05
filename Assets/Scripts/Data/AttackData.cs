using Unity.Entities;
using Unity.Mathematics;
[GenerateAuthoringComponent]
public struct AttackData : IComponentData
{
    public float3 attackPoint;
    public double lastAttackTime;
    public double fireRate;
}
