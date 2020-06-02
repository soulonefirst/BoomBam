using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Mathematics;

public class Shooting : SystemBase
{
    double time;
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .WithStructuralChanges()
            .ForEach((in AttackInputPosition attackPosition,in Translation translation, in Bullet bullet) => 
            {
                var EM = World.DefaultGameObjectInjectionWorld.EntityManager;
                
                if(attackPosition.Value.x != 0 && Time.ElapsedTime - time > 0.5 )
                {
                    var b = EM.Instantiate(bullet.Value);
                    EM.SetComponentData(b, new TargetPosition { targetPosition = attackPosition.Value, startPosition = translation.Value });
                    
                    EM.SetComponentData(b, new Translation { Value = translation.Value });
                    time = Time.ElapsedTime;
                }
            }).Run();
    }
}
