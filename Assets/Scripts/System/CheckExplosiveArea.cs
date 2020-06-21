using Unity.Entities;
using Unity.Transforms;
[UpdateBefore(typeof(Spawner))]
public class CheckExplosiveArea : SystemBase
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
                    SD.numEntityToSpawn = 6;
                    SD.moveData = new MoveData { startPosition = translation.Value, targetPosition = translation.Value, nonStop = 0 };
                    SD.color = color.Value;
                    ETS.Add(SD);
                }
            }).Schedule();
    }
}
