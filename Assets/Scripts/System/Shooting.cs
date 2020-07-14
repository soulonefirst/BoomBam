using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;
using Unity.Rendering;
[UpdateAfter(typeof(InputManager))]
public class Shooting : SystemBase
{    protected override void OnUpdate()
    {
        var time = Time.ElapsedTime;
        var screenOneThird = InputCatcherSetter.screenHight * 2 / 3;
        var sreenBottom = -InputCatcherSetter.screenHight;
        var ETS = EntityManager.GetBuffer<EntityToSpawnData>(GetSingletonEntity<EntityToSpawnData>());
        Entities
            .ForEach((ref AttackData attackPosition, in Translation translation ) => 
            {
                if (attackPosition.attackPoint.x != 0 && time - attackPosition.lastAttackTime > attackPosition.fireRate )
                {
                    var SD = new SpawnData();
                    if(translation.Value.y <= sreenBottom + screenOneThird)
                    {
                        SD.numPrefabToSpawn = 0;
                    } else if(translation.Value.y <= sreenBottom + screenOneThird * 2)
                    {
                        SD.numPrefabToSpawn = 1;
                    } else
                        SD.numPrefabToSpawn = 2;
                    SD.moveData = new MoveData { startPosition = translation.Value, targetPosition = attackPosition.attackPoint, nonStop = 1 };
                    ETS.Add(SD);
                    attackPosition.lastAttackTime = time;
                }
            }).Schedule();
        Dependency.Complete();
     }
}
