using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
[UpdateAfter(typeof(InputManager))]

public class Move : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        Entities
            .ForEach((ref TargetPosition targetPosition,ref PhysicsVelocity velocity, ref Translation trans, in Speed speed) =>
            {
                if (targetPosition.targetPosition.Equals(float3.zero))
                    return;

                float3 dir = math.normalize(targetPosition.targetPosition - targetPosition.startPosition);
                velocity.Linear = dir*speed.Value * deltaTime;
            }).Schedule();
        Dependency.Complete();
    }
}
