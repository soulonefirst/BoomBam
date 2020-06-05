using Unity.Entities;

public class TakeDamage : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .ForEach((ref TakeDamageData damage, ref Hp hp) => {
                if(damage.previousSorce.Index != damage.Value.sorceEntity.Index)
                {
                    hp.Value -= damage.Value.damage;
                    damage.previousSorce = damage.Value.sorceEntity;
                }
            }).Schedule();
    }
}
