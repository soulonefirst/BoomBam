
using Unity.Entities;
[GenerateAuthoringComponent]
public struct EnemiesSpawnSettingsData : IComponentData
{
    public int lvl;
    public int allEnemies;
    public int strongEnrmies;
    public float spawnSpeed;
    public int maxSpawnAtTime;
    public int redEnemy;
    public int blueEnemy;
    public int yellowEnemy;

}
