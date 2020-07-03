using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
public class MoveObjectToBorder :MonoBehaviour, IConvertGameObjectToEntity
{
    [Tooltip("up=1,down=2,right=3")]
    public int position;
    public InputCatcherSetter inputCatcher;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        inputCatcher.Initialization();
         switch (position)
        {
            case 1:
                dstManager.SetComponentData(entity, new Translation { Value = new float3(InputCatcherSetter.screenWidth / 2, InputCatcherSetter.screenHight, 0 )});
                break;
            case 2:
                dstManager.SetComponentData(entity, new Translation { Value = new float3(InputCatcherSetter.screenWidth / 2, 0, 0) });
                break;
            case 3:
                dstManager.SetComponentData(entity, new Translation { Value = new float3(InputCatcherSetter.screenWidth, InputCatcherSetter.screenHight / 2, 0) });
                break;

        }
    }



}
