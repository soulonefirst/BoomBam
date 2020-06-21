using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using UnityEngine;
using Unity.Rendering;
[UpdateBefore(typeof(Spawner))]
public class Shooting : SystemBase
{    protected override void OnUpdate()
    {
        var time = Time.ElapsedTime;
        var screenOneThird = InputCatcherSetter.screenHight / 3;
        var ETS = EntityManager.GetBuffer<EntityToSpawnData>(GetSingletonEntity<EntityToSpawnData>());
        Entities
            .ForEach((ref AttackData attackPosition, in Translation translation ) => 
            {
                if (attackPosition.attackPoint.x != 0 && time - attackPosition.lastAttackTime > attackPosition.fireRate )
                {
                    var SD = new SpawnData();
                    if(translation.Value.y <= screenOneThird)
                    {
                        SD.numEntityToSpawn = 0;
                    } else if(translation.Value.y <= screenOneThird * 2)
                    {
                        SD.numEntityToSpawn = 1;
                    } else
                        SD.numEntityToSpawn = 2;
                    SD.moveData = new MoveData { startPosition = translation.Value, targetPosition = attackPosition.attackPoint, nonStop = 1 };
                    ETS.Add(SD);
                    attackPosition.lastAttackTime = time;
                }
            }).Schedule();
        Dependency.Complete();
     }
}
