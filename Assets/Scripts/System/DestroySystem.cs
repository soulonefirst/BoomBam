using Unity.Entities;
public class DestoySystem : SystemBase
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
            .ForEach((int entityInQueryIndex, Entity entity, in DestroyData dead) =>
            {
                if(dead.Value == true)
                {
                   ecb.DestroyEntity(entityInQueryIndex, entity);
                }
            }).ScheduleParallel();

        m_EndSimulationEcbSystem.AddJobHandleForProducer(Dependency);
    }
}