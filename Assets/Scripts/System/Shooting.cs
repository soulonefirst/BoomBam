using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;

public class Shooting : SystemBase
{
    protected override void OnUpdate()
    {
        var time = Time.ElapsedTime;
        Entities
            .ForEach((ref SpawnData spawnData, ref AttackData attackPosition, in Translation translation ) => 
            {

                if (attackPosition.attackPoint.x != 0 && time - attackPosition.lastAttackTime > attackPosition.fireRate )
                {
                    spawnData.moveData = new MoveData { startPosition = translation.Value, targetPosition = attackPosition.attackPoint, nonStop = 1 };
                    spawnData.damageData = new DamageData { damage = 1 };
                    spawnData.alreadySpawn = false;
                    attackPosition.lastAttackTime = time;
                }
            }).Schedule();
     }
}
