using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Jobs;
using Unity.Burst;
using Unity.Rendering;
using Unity.Collections;

public class CollisionManager : SystemBase
{
    
    [BurstCompile]
    public struct CollisionJob : ICollisionEventsJob
    {
        public ComponentDataFromEntity<DamageData> damageData;
        public ComponentDataFromEntity<TakeDamageData> takeDamage;
        public ComponentDataFromEntity<DestroyData> destroyData;
        public ComponentDataFromEntity<ColorData> colorData;
        public void Execute(CollisionEvent collisionEvent)
        {
            Entity attacker;
            Entity target;
            if ((damageData.HasComponent(collisionEvent.Entities.EntityA) && takeDamage.HasComponent(collisionEvent.Entities.EntityB))
                || (damageData.HasComponent(collisionEvent.Entities.EntityB) && takeDamage.HasComponent(collisionEvent.Entities.EntityA)))
            {
                attacker = damageData.HasComponent(collisionEvent.Entities.EntityA) ? collisionEvent.Entities.EntityA : collisionEvent.Entities.EntityB;
                target = takeDamage.HasComponent(collisionEvent.Entities.EntityA) ? collisionEvent.Entities.EntityA : collisionEvent.Entities.EntityB;
                if (colorData[attacker].Value == colorData[target].Value)
                {

                    TakeDamageData takeDamageData = new TakeDamageData { Value = damageData[attacker] };
                    takeDamage[target] = takeDamageData;
                }
                destroyData[attacker] = new DestroyData { Value = true };
            }

        }
    }
    [ReadOnly]
    private BuildPhysicsWorld buildPhysicsWorld;
    [ReadOnly]
    private StepPhysicsWorld stepPhysicsWorld;

    protected override void OnCreate()
    {
        buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    protected override void OnUpdate()
    {
        CollisionJob triggerJob = new CollisionJob
        {
            damageData = GetComponentDataFromEntity<DamageData>(),
            takeDamage = GetComponentDataFromEntity<TakeDamageData>(),
            destroyData = GetComponentDataFromEntity<DestroyData>(),
            colorData = GetComponentDataFromEntity<ColorData>()
        };
        JobHandle job = triggerJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);
        job.Complete();

    }

}
