using Unity.Entities;
using Unity.Physics;
using Unity.Transforms;
using Unity.Mathematics;
[UpdateAfter(typeof(InputManager))]

public class Move : SystemBase
{
    protected override void OnUpdate()
    {
        var deltaTime = Time.DeltaTime;
        Entities
            .ForEach((ref PhysicsVelocity velocity, in Translation trans, in Speed speed,  in TargetPosition targetPosition) =>
            {
                float speedX = 0;
                float speedY = 0;
                if (math.abs(trans.Value.y - targetPosition.Value.y) > 0.03)
                    speedY = trans.Value.y < targetPosition.Value.y ? speed.Value : -speed.Value;
                if(math.abs(trans.Value.x - targetPosition.Value.x) > 0.03)
                    speedX = trans.Value.x < targetPosition.Value.x ? speed.Value : -speed.Value;
                    velocity.Linear = new float3(math.abs(trans.Value.x - targetPosition.Value.x) * speedX, math.abs(trans.Value.y - targetPosition.Value.y) * speedY, 0) * deltaTime;
            }).Schedule();
        Dependency.Complete();
    }
}
