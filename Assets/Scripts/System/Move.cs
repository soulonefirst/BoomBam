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
            .ForEach((Entity entity, ref MoveData moveData,ref Translation translation, in Speed speed) =>
            {
                if (moveData.nonStop == 2 || moveData.targetPosition.Equals(float3.zero))
                    return;
                float3 dir = float3.zero;
                if (moveData.nonStop == 1)
                {
                     dir = math.normalize(moveData.targetPosition - moveData.startPosition);
                     moveData.nonStop = 2;
                }
                else
                    dir = math.normalize(moveData.targetPosition - translation.Value) * deltaTime;
                if (HasComponent<PhysicsVelocity>(entity))
                {
                    SetComponent(entity, new PhysicsVelocity { Linear = dir * speed.Value });                    
                } else
                  translation.Value += dir * speed.Value * deltaTime;
            }).Schedule();
    }
}
