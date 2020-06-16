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
            .ForEach((ref PhysicsVelocity velocity, ref MoveData moveData,in Speed speed, in Translation translation) =>
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
                velocity.Linear = dir* speed.Value;
            }).Schedule();
    }
}
