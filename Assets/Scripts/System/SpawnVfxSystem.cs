using Unity.Collections;
using Unity.Entities;
public class SpawnVfxSystem : SystemBase
{
    protected override void OnCreate()
    {
        SpawningVFX.Initialization();
    }
    protected override void OnUpdate()
    {  
        Entities
            .WithoutBurst()
            .ForEach((Entity entity, ref IdVfxData idVfx) => {
            if (idVfx.Value >= 0)
            {
                SpawningVFX.VFXSpawn(entity, idVfx.Value);
                idVfx.Value = -1;
            }
        }).Run();
    }

}
