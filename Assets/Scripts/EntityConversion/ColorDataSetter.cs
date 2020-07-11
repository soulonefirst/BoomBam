using Unity.Entities;
using UnityEngine;
using UnityEngine.VFX;
public class ColorDataSetter : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new ColorData { Value = GetComponent<VisualEffect>().GetInt("ColorId") });
    }
}
