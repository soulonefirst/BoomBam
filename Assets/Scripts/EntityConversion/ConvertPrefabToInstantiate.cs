using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Mathematics;

public class ConvertPrefabToInstantiate : MonoBehaviour, IDeclareReferencedPrefabs,IConvertGameObjectToEntity 
{
    public GameObject[] prefabs;

    public InputCatcherSetter inputCatcher;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var buffer = dstManager.AddBuffer<PrefabsData>(entity);
        for (int i =0; i< prefabs.Length;i++)
        {
            buffer.Add(conversionSystem.GetPrimaryEntity(prefabs[i]));
        }

        dstManager.AddBuffer<EntityToSpawnData>(entity);

        inputCatcher.Initialization();
        var ecb = dstManager.World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>().CreateCommandBuffer();
        //downborder
        ecb.SetComponent(ecb.Instantiate(dstManager.GetBuffer<PrefabsData>(entity)[8].Value), new Translation { Value = new float3(0, -InputCatcherSetter.screenHight, 0) });
        //upborder
        ecb.SetComponent(ecb.Instantiate(dstManager.GetBuffer<PrefabsData>(entity)[9].Value), new Translation { Value = new float3(0, InputCatcherSetter.screenHight-0.5f, 0) });

        //spot
        for(float i = -InputCatcherSetter.screenHight; i < InputCatcherSetter.screenHight-1; i++)
        {

            ecb.SetComponent(ecb.Instantiate(dstManager.GetBuffer<PrefabsData>(entity)[10].Value), new Translation { Value = new float3(InputCatcherSetter.screenWidth, i+0.5f, 0) });
        }

    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        foreach(GameObject pref in prefabs)
        {
            referencedPrefabs.Add(pref);
        }
    }
}
