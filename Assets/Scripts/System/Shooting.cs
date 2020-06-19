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
        Entities
            .ForEach((Entity entity, ref SpawnData spawnData, ref AttackData attackPosition, in Translation translation ) => 
            {
                if (attackPosition.attackPoint.x != 0 && time - attackPosition.lastAttackTime > attackPosition.fireRate )
                {
                    if(translation.Value.y <= screenOneThird)
                    {
                        spawnData.numEntityToSpawn = 0;
                    } else if(translation.Value.y <= screenOneThird * 2)
                    {
                        spawnData.numEntityToSpawn = 1;
                    } else
                        spawnData.numEntityToSpawn = 2;
                    spawnData.moveData = new MoveData { startPosition = translation.Value, targetPosition = attackPosition.attackPoint, nonStop = 1 };
                    spawnData.alreadySpawn = false;
                    attackPosition.lastAttackTime = time;
                }
            }).Schedule();
        Dependency.Complete();
     }
}
