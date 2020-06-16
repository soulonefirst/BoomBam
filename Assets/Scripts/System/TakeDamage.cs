using Unity.Entities;

public class TakeDamage : SystemBase
{
    protected override void OnUpdate()
    {
        Entities
            .ForEach((ref TakeDamageData damage, ref Hp hp, ref DestroyData destroy) =>
            {
                if(damage.Value.damage != 0)
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
