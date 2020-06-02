using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class InputManager : SystemBase
{
    protected override void OnUpdate()
    {
        var screenWidthPercent = InputCatcherSetter.screenWidth / 100;
        var screenHight = InputCatcherSetter.screenHight;
        Entities
            .ForEach((ref AttackInputPosition AIP, ref TargetPosition TP, ref Speed speed, in DynamicBuffer<InputDataPosition> IDPs, in Translation trans, in GunMoveSettings settings) =>
            {
                AIP.Value = float3.zero;
                for (int i = 0; i < IDPs.Length; i++)
                {
                    TP.startPosition = trans.Value;
                    if (IDPs[i].Value.x != 0)
                    {
                        if (IDPs[i].Value.x < screenWidthPercent * 30)
                        {
                            TP.targetPosition = new float3(trans.Value.x, IDPs[i].Value.y, 0);
                            speed.Value = math.smoothstep(-settings.startSpeed, math.abs(-screenHight - screenHight), math.abs(trans.Value.y - IDPs[i].Value.y)) * settings.maxSpeed;
                        }
                        else
                            AIP.Value = IDPs[i].Value;
                    }
                }

            }).Run();
    }
}
