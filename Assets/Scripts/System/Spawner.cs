using Unity.Entities;
using Unity.Transforms;
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
        var time = Time.ElapsedTime;
        var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer();
        Entities
            .ForEach((ref DynamicBuffer <EntityToSpawnData> ETS, in DynamicBuffer<PrefabsData> PD) =>
            {
                if(ETS.Length != 0)
                {
                    for(int i = 0; i < ETS.Length; i++)
                    {
                        ecb.SetComponent(PD[ETS[i].Value.numPrefabToSpawn].Value, new Translation {Value = ETS[i].Value.moveData.startPosition});
                        var b = ecb.Instantiate(PD[ETS[i].Value.numPrefabToSpawn].Value);
                        ecb.SetComponent(b, ETS[i].Value.moveData);
                        if (ETS[i].Value.color != 0 )
                        {
                            ecb.SetComponent(b, new ColorData { Value = ETS[i].Value.color});
                        }
                        ETS.RemoveAt(i);
                    }
                }
            }).Schedule();
        m_EndSimulationEcbSystem.AddJobHandleForProducer(Dependency);
        Dependency.Complete();

        
    }
}
