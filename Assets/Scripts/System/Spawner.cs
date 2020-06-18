using Unity.Entities;
using Unity.Transforms;
[UpdateBefore(typeof(DestoySystem))]
public class Spawner : SystemBase
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
        Entities
            .ForEach((int entityInQueryIndex, ref SpawnData spawnData,ref Translation translation) =>
            {
                if (!spawnData.alreadySpawn)
                {
                    var b = ecb.Instantiate(entityInQueryIndex, spawnData.entityToSpawn);
                    ecb.SetComponent(entityInQueryIndex, b, new Translation { Value = spawnData.moveData.startPosition });
                    ecb.SetComponent(entityInQueryIndex, b, spawnData.moveData);
                    ecb.SetComponent(entityInQueryIndex, b, spawnData.damageData);
                    spawnData.alreadySpawn = true;
                }
            }).ScheduleParallel();

        m_EndSimulationEcbSystem.AddJobHandleForProducer(Dependency);
    }
}
