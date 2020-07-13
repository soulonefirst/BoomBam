using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Jobs;
using Unity.Burst;
using Unity.Rendering;
using Unity.Collections;
[UpdateBefore(typeof(TakeDamage))]
public class CollisionManager : SystemBase
{
    
    [BurstCompile]
    public struct CollisionJob : ICollisionEventsJob
    {
        public ComponentDataFromEntity<DamageData> damageData;
        public ComponentDataFromEntity<TakeDamageData> takeDamage;
        public ComponentDataFromEntity<DestroyData> destroyData;
        public ComponentDataFromEntity<ColorData> colorData;
        public EntityManager EM;
        public void Execute(CollisionEvent collisionEvent)
        {
            if ((damageData.HasComponent(collisionEvent.EntityA) && takeDamage.HasComponent(collisionEvent.EntityB))
                || (damageData.HasComponent(collisionEvent.EntityB) && takeDamage.HasComponent(collisionEvent.EntityA)))
            {
                Entity attacker;
                Entity target;
                attacker = damageData.HasComponent(collisionEvent.EntityA) ? collisionEvent.EntityA : collisionEvent.EntityB;
                target = takeDamage.HasComponent(collisionEvent.EntityA) ? collisionEvent.EntityA : collisionEvent.EntityB;
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
            colorData = GetComponentDataFromEntity<ColorData>(),
            EM = EntityManager
        };
        JobHandle job = triggerJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);
        job.Complete();

    }

}
