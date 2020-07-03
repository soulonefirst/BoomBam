using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
[UpdateBefore(typeof(EnemyDeathSystem))]
public class TakeDamage : SystemBase
{
    protected override void OnUpdate()
    {
        var EM = EntityManager;
        Entities
            .ForEach((Entity entity, ref TakeDamageData damage, ref Hp hp, ref DestroyData destroy) =>
            {
                    if(damage.Value.damage > 0)
                    {                    
                        hp.Value -= damage.Value.damage;
                        if (hp.Value < 1)
                        {
                            destroy.Value = true;
                        }
                        damage = new TakeDamageData();                  
                    }
            }).Schedule();
    }
}
