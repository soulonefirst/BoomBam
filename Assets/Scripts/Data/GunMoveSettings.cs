using Unity.Entities;
[GenerateAuthoringComponent]
public struct GunMoveSettings : IComponentData
{
    public float maxSpeed;
    public float minSpeed;
}
