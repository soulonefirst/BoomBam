using System;
using Unity.Entities;
[GenerateAuthoringComponent]
public struct LifeTimeData : IComponentData
{
    public double destroyTime;
    public double lifeTime;
}
