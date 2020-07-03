using Unity.Entities;
[UpdateAfter(typeof(Spawner))]
public class LifeTimeSystem : SystemBase
{
    protected override void OnUpdate()
    {
        var curTime = Time.ElapsedTime;
      
        Entities.ForEach((ref LifeTimeData lifeTime, ref DestroyData destroy) => {
            if (lifeTime.destroyTime == 0)
            {
                lifeTime.destroyTime = lifeTime.lifeTime + curTime;
            }
            if(lifeTime.destroyTime < curTime)
            {
                destroy.Value = true;
            }
        }).Schedule();
    }
}
