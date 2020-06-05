using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Physics;

public class Shooting : SystemBase
{
    EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;
    protected override void OnCreate()
    {
        m_EndSimulationEcbSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer().ToConcurrent();
        var time = Time.ElapsedTime;
        Entities
            .ForEach((int entityInQueryIndex, ref AttackData attackPosition, in Translation translation, in Bullet bullet) => 
            {

                if (attackPosition.attackPoint.x != 0 && time - attackPosition.lastAttackTime > attackPosition.fireRate )
                {

                    var b = ecb.Instantiate(entityInQueryIndex, bullet.Value);
                    ecb.SetComponent(entityInQueryIndex, b, new MoveData { targetPosition = attackPosition.attackPoint, startPosition = translation.Value, nonStop = 1 }); ;
                    ecb.SetComponent(entityInQueryIndex, b, new Translation { Value = translation.Value });
                    ecb.SetComponent(entityInQueryIndex, b, new DamageData { damage = 5,sorceEntity =b });
                    attackPosition.lastAttackTime = time;
                }
            }).ScheduleParallel();
        m_EndSimulationEcbSystem.AddJobHandleForProducer(Dependency);
    }
}
