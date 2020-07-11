using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Collections;
public class InputManager : SystemBase
{
    protected override void OnUpdate()
    {
        var screenWidth = InputCatcherSetter.screenWidth;
        var screenWidthPercent =  screenWidth/50;
        var screenHight = InputCatcherSetter.screenHight;
        Entities
            .ForEach((ref AttackData AIP, ref MoveData MD, ref Speed speed, in DynamicBuffer<InputDataPosition> IDPs, in Translation trans, in GunMoveSettings settings) =>
            {
                //сброс позиции атаки
                AIP.attackPoint = float3.zero;
                //приравнивание текущей позиции с начальной позицией
                MD.startPosition = trans.Value;
                //скорость в зависимости от растояния между текущей и заданой позицией
                speed.Value = math.lerp(settings.minSpeed, settings.maxSpeed, math.abs(MD.targetPosition.y - MD.startPosition.y));
                for (int i = 0; i < IDPs.Length; i++)
                {
                    if (IDPs[i].Value.x != 0)
                    {
                        //разделение экрана на зоны движения и стерльбы
                        if (IDPs[i].Value.x < -screenWidth + (screenWidthPercent * 20)) 
                        {
                            //назначение заданой позиции для движения
                           MD.targetPosition = new float3(trans.Value.x, IDPs[i].Value.y, 0);
                        }
                        else
                            //назначение координат атаки
                            AIP.attackPoint = IDPs[i].Value;
                    }
                }

            }).Run();
    }
}
