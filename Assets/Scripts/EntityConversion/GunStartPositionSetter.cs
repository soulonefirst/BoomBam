using Unity.Transforms;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public class GunStartPositionSetter : MonoBehaviour, IConvertGameObjectToEntity
{
    public InputCatcherSetter inputCatcher;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        inputCatcher.Initialization();
        dstManager.SetComponentData(entity, new Translation { Value = new float3(-InputCatcherSetter.screenWidth + 2, InputCatcherSetter.screenHight / 2, 0) });
        dstManager.SetComponentData(entity, new MoveData { startPosition = new float3(-InputCatcherSetter.screenWidth + 2, InputCatcherSetter.screenHight / 2, 0), targetPosition= new float3(-InputCatcherSetter.screenWidth + 2, InputCatcherSetter.screenHight / 2, 0) });
    }
}
