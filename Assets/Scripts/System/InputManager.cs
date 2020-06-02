using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
public class InputManager : SystemBase
{
    protected override void OnUpdate()
    {
        var screenWidthPercent = InputCatcherSetter.screenWidthPercent;
        var screenHight = InputCatcherSetter.screenHight;
        Entities
            .ForEach((ref AttackInputPosition AIP, ref TargetPosition TP, ref Speed speed, in DynamicBuffer<InputDataPosition> IDPs,in Translation trans, in GunMoveSettings settings) =>
            {
                AIP.Value = float3.zero;
                for (int i = 0; i < IDPs.Length; i++)
                {
                    if (IDPs[i].Value.x != 0)
                    {
                        //процент от левой части экраны
                        if (IDPs[i].Value.x < -(screenWidthPercent * 50))
                        {
                            TP.Value = new float3(TP.Value.x, IDPs[i].Value.y, 0);
                            speed.Value = math.smoothstep(-settings.startSpeed, math.abs(-screenHight -screenHight), math.abs(trans.Value.y - IDPs[i].Value.y)) * settings.maxSpeed;
                        }
                        else
                            AIP.Value = IDPs[i].Value;
                    }

                }

            }).Run();
    }
}
