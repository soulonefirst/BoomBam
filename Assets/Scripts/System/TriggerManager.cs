using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;
[UpdateBefore(typeof(CollisionManager))]
public class TriggerManager : SystemBase
{
    [BurstCompile]
    public struct TriggerJob : ITriggerEventsJob
    {
        public ComponentDataFromEntity<DamageData> damageData;
        public ComponentDataFromEntity<TakeDamageData> takeDamage;
        public ComponentDataFromEntity<DestroyData> destroyData;
        public ComponentDataFromEntity<ColorData> colorData;
        public ComponentDataFromEntity<FreeSpotData> freeSpot;
        public void Execute(TriggerEvent triggerEvent)
        {
            if ((damageData.HasComponent(triggerEvent.EntityA) && takeDamage.HasComponent(triggerEvent.EntityB))
                || (damageData.HasComponent(triggerEvent.EntityB) && takeDamage.HasComponent(triggerEvent.EntityA)))
            {
                Entity attacker = damageData.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA : triggerEvent.EntityB;
                Entity target = takeDamage.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA : triggerEvent.EntityB;
                if (colorData.HasComponent(triggerEvent.EntityA) && colorData.HasComponent(triggerEvent.EntityB))
                {
                    if (colorData[attacker].Value == colorData[target].Value)
                    {
                        TakeDamageData takeDamageData = new TakeDamageData { Value = damageData[attacker] };
                        takeDamage[target] = takeDamageData;
                    }
                }
                else if (!colorData.HasComponent(triggerEvent.EntityA) && !colorData.HasComponent(triggerEvent.EntityB))
                {
                    TakeDamageData takeDamageData = new TakeDamageData { Value = damageData[attacker] };
                    takeDamage[target] = takeDamageData;
                }
                //destroyData[attacker] = new DestroyData { Value = true };
                if (freeSpot.HasComponent(triggerEvent.EntityA) || freeSpot.HasComponent(triggerEvent.EntityB))
                {
                    Entity spot = freeSpot.HasComponent(triggerEvent.EntityA) ? triggerEvent.EntityA : triggerEvent.EntityB;
                    freeSpot[spot] = new FreeSpotData { Value = false };
                }
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
            Entities
                .ForEach((ref FreeSpotData freeSpot) =>
                {
                    freeSpot.Value = true;
                }).Schedule();
            TriggerJob triggerJob = new TriggerJob
            {
                damageData = GetComponentDataFromEntity<DamageData>(),
                takeDamage = GetComponentDataFromEntity<TakeDamageData>(),
                destroyData = GetComponentDataFromEntity<DestroyData>(),
                colorData = GetComponentDataFromEntity<ColorData>(),
                freeSpot = GetComponentDataFromEntity<FreeSpotData>()
            };
            JobHandle job = triggerJob.Schedule(stepPhysicsWorld.Simulation, ref buildPhysicsWorld.PhysicsWorld, Dependency);
            job.Complete();

        }

    }


