using Unity.Entities;
using Unity.Mathematics;
[GenerateAuthoringComponent]
public struct InputDataPosition : IBufferElementData
{
    public float3 Value;

    public static implicit operator InputDataPosition(float3 e)
    {
        return new InputDataPosition { Value = e };
    }
}
