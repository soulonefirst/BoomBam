using Unity.Entities;
using Unity.Mathematics;
[GenerateAuthoringComponent]
public struct MoveData : IComponentData
{
    public float3 targetPosition;
    public float3 startPosition;
    public float nonStop;
}
