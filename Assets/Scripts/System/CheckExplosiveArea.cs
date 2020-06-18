using Unity.Entities;
using Unity.Transforms;
[UpdateBefore(typeof(Spawner))]
public class CheckExplosiveArea : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .ForEach(( ref SpawnData spawnData, in DestroyData destroy,in Translation translation) =>
            {
                if(destroy.Value == true)
                {
                    spawnData.moveData = new MoveData { startPosition = translation.Value, targetPosition = translation.Value, nonStop = 0 };
                    spawnData.damageData = new DamageData { damage = 1 };
                    spawnData.alreadySpawn = false;
                }
            }).Schedule();
    }
}
