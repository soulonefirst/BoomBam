using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class Shooting : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .WithoutBurst()
            .WithStructuralChanges()
            .ForEach((in AttackInputPosition attackPosition,in Translation translation, in Bullet bullet) => 
            {
                var EM = World.DefaultGameObjectInjectionWorld.EntityManager;
                if(attackPosition.Value.x != 0)
                {
                    var b = EM.Instantiate(bullet.Value);
                    EM.SetComponentData(b, new TargetPosition { Value = attackPosition.Value });
                    EM.SetComponentData(b, new Translation { Value = translation.Value });

                }
            }).Run();
    }
}
