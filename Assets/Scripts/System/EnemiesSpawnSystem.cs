using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
public class EnemiesSpawnSystem : SystemBase
{
    private int lvl;
    private int allEnemiesToSpawn;
    private int strongEnemies;
    private float restTime;
    private float spawnRate;
    private float spawnTime;
    private int maxSpawnAtTime;
    private Random random;
    protected override void OnCreate()
    {
        random = new Random();
        random.InitState(unchecked((uint)System.DateTime.Now.Ticks));
        lvl = 1;
        allEnemiesToSpawn = 10;
        strongEnemies = 1;
        maxSpawnAtTime = 2;
        spawnRate = 0.5f;
        restTime = 5f;
    }
    protected override void OnUpdate()
    {
        var freeSpots = new NativeList<Entity>(Allocator.TempJob); 

        var ETS = EntityManager.GetBuffer<EntityToSpawnData>(GetSingletonEntity<EntityToSpawnData>());
        if (spawnTime < Time.ElapsedTime)
        {
            Entities
                .ForEach((Entity entity, in FreeSpotData freeSpot) => {

                    if (freeSpot.Value)
                    freeSpots.Add(entity);
                }).Schedule();
            Dependency.Complete();
            if(freeSpots.Length > 0)
            {
                for (int i = 0; i < maxSpawnAtTime; i++)
                {
                    
                    if (allEnemiesToSpawn > 0 && freeSpots.Length != 0)
                    {
                        var r = random.NextInt(freeSpots.Length);
                        var SD = new SpawnData();
                        var freeSpotTtrans = GetComponent <Translation> (freeSpots[r]).Value;
                        SD.numPrefabToSpawn = random.NextInt(3,6);
                        SD.moveData = new MoveData { startPosition = freeSpotTtrans, targetPosition = new float3(freeSpotTtrans.x - 30, freeSpotTtrans.y,0), nonStop = 0 };
                        ETS.Add(SD);
                        freeSpots.RemoveAtSwapBack(r);
                        allEnemiesToSpawn--;
                    }

                }
            }

            spawnTime = spawnRate + (float)Time.ElapsedTime;
            if(allEnemiesToSpawn < 1)
            {
                spawnTime = restTime + (float)Time.ElapsedTime;
                lvl++;
                allEnemiesToSpawn = 10 + lvl;
            }
        }
            freeSpots.Dispose();
    }
}
