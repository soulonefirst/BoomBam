using Unity.Entities;
using UnityEngine;
using Unity.Rendering;

public class ColorDataSetter : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new ColorData { Value = dstManager.GetSharedComponentData<RenderMesh>(entity).material.color });
    }
}
