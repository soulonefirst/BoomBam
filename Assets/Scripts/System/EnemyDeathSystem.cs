using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;
[UpdateBefore(typeof(DestoySystem))]
public class EnemyDeathSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var ETS = EntityManager.GetBuffer<EntityToSpawnData>(GetSingletonEntity<EntityToSpawnData>());
        Entities
            .WithAll<EnemyTag>()
            .ForEach((in DestroyData destroy,in Translation translation, in ColorData color) =>
            {
                if(destroy.Value == true)
                {
                    var SD = new SpawnData();
                    SD.numPrefabToSpawn = 7;
                    SD.moveData = new MoveData { startPosition = translation.Value, targetPosition = translation.Value, nonStop = 0 };
                    SD.color = color.Value;
                    ETS.Add(SD);
                    var SD2 = new SpawnData();
                    SD2.numPrefabToSpawn = 6;
                    SD2.moveData = new MoveData { startPosition = translation.Value, targetPosition = new float3(translation.Value.x - 30, translation.Value.y, 0), nonStop = 0 };
                    ETS.Add(SD2);
                }
            }).Schedule();
    }
}
