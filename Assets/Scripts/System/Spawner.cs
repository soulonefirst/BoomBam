using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;
using Unity.Collections;
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
        var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer();

        Entities
            .ForEach((ref DynamicBuffer <EntityToSpawnData> ETS, in DynamicBuffer<PrefabsData> PD) =>
            {
                if(ETS.Length != 0)
                {
                    for(int i = 0; i < ETS.Length; i++)
                    {
                        var b = ecb.Instantiate(PD[ETS[i].Value.numEntityToSpawn].Value);                    
                        ecb.SetComponent(b, new Translation { Value = ETS[i].Value.moveData.startPosition });
                        ecb.SetComponent(b, ETS[i].Value.moveData);

                        if (!ETS[i].Value.color.Equals(Color.clear))
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
