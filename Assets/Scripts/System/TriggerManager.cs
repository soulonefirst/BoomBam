using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Jobs;
using Unity.Burst;

public class TriggerManager : SystemBase
{
    [BurstCompile]
    public struct TriggerJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<DamageData> damageData;
        public ComponentDataFromEntity<TakeDamageData> takeDamage;
        public ComponentDataFromEntity<DestroyData> destroyData;
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity attacker;
            Entity target;
            if ((damageData.HasComponent(triggerEvent.Entities.EntityA) && takeDamage.HasComponent(triggerEvent.Entities.EntityB))
                || (damageData.HasComponent(triggerEvent.Entities.EntityB) && takeDamage.HasComponent(triggerEvent.Entities.EntityA)))
            {
                attacker = damageData.HasComponent(triggerEvent.Entities.EntityA) ? triggerEvent.Entities.EntityA : triggerEvent.Entities.EntityB;
                target = takeDamage.HasComponent(triggerEvent.Entities.EntityA) ? triggerEvent.Entities.EntityA : triggerEvent.Entities.EntityB;
                TakeDamageData takeDamageData = new TakeDamageData { Value = damageData[attacker] };
                takeDamage[target] = takeDamageData;
                destroyData[attacker] = new DestroyData { Value = true };
            }

        }
    }

    private BuildPhysicsWorld buildPhysicsWorld;
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    protected override void OnUpdate()
    {
        TriggerJob triggerJob = new TriggerJob
        {
            damageData = GetComponentDataFromEntity<DamageData>(),
            takeDamage = GetComponentDataFromEntity<TakeDamageData>(),
            destroyData = GetComponentDataFromEntity<DestroyData>()
        };
        JobHandle job = triggerJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);
        job.Complete();

    }

}

