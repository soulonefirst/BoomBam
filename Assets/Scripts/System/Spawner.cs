using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using UnityEngine;
using Unity.Collections;
[UpdateBefore(typeof(DestoySystem))]
public class Spawner : SystemBase
{
    EndSimulationEntityCommandBufferSystem m_EndSimulationEcbSystem;
    Entity entityPrefabs;
    NativeArray<Entity> entities;
    protected override void OnCreate()
    {
        m_EndSimulationEcbSystem = World
            .GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    protected override void OnUpdate()
    {
        if(entities.Length == 0)
        {
            entities = World.EntityManager.GetAllEntities(Allocator.Temp);
            foreach (Entity entity in entities)
            {
                if (EntityManager.HasComponent<PrefabsData>(entity))
                {
                    entityPrefabs = entity;
                        Debug.Log(entities.Length);
                }
            }

        }
        var EP = entityPrefabs;
        var EM = EntityManager;
        var ecb = m_EndSimulationEcbSystem.CreateCommandBuffer();

        Entities
            .ForEach((Entity entity,int entityInQueryIndex, ref SpawnData spawnData) =>
            {
            if (!spawnData.alreadySpawn)
            {
                var b = ecb.Instantiate(EM.GetBuffer<PrefabsData>(EP)[spawnData.numEntityToSpawn].Value);                    
                    ecb.SetComponent(b, new Translation { Value = spawnData.moveData.startPosition });
                    ecb.SetComponent(b, spawnData.moveData);
                    spawnData.alreadySpawn = true;
                    if (!spawnData.color.Equals(Color.clear))
                    {
                        ecb.SetComponent(b, new ColorData { Value = spawnData.color});
                    }
                }
            }).Schedule();
        m_EndSimulationEcbSystem.AddJobHandleForProducer(Dependency);
        Dependency.Complete();

        
    }
}
