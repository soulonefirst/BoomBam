using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;

public class RotateTo : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .ForEach((ref Rotation rotation, in RotateToData rotateTo, in PhysicsVelocity velocity, in Translation translation) =>
            {
                if (!rotateTo.Value.Equals(float3.zero))
                {
                    float3 dir = math.normalize(rotateTo.Value -translation.Value);
                    rotation.Value = quaternion.RotateZ(math.atan2(dir.y, dir.x) + math.radians(90));
                }
                else
                    rotation.Value = quaternion.RotateZ(math.atan2(velocity.Linear.y, velocity.Linear.x) + math.radians(90));

            }).Schedule();
    }
}
