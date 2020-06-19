using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class ConvertPrefabToInstantiate : MonoBehaviour, IDeclareReferencedPrefabs,IConvertGameObjectToEntity 
{
    public GameObject[] prefabs;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var buffer = dstManager.AddBuffer<PrefabsData>(entity);
        for (int i =0; i< prefabs.Length;i++)
        {
            buffer.Add(conversionSystem.GetPrimaryEntity(prefabs[i]));
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
