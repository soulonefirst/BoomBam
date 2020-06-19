using Unity.Entities;
using Unity.Transforms;
[UpdateBefore(typeof(Spawner))]
public class CheckExplosiveArea : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .ForEach(( ref SpawnData spawnData, in DestroyData destroy,in Translation translation, in ColorData color) =>
            {
                if(destroy.Value == true)
                {
                    spawnData.numEntityToSpawn = 6;
                    spawnData.moveData = new MoveData { startPosition = translation.Value, targetPosition = translation.Value, nonStop = 0 };
                    spawnData.alreadySpawn = false;
                    spawnData.color = color.Value;
                }
            }).Schedule();
    }
}
