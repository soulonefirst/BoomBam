using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.VFX;
using System.Collections;

public class VFXTransformRelation : MonoBehaviour
{
    public Entity ent;
    private bool damagable;
    EntityManager EM;
    VisualEffect enemy;
    private float ssmultAdd;
    private float ssrAdd;
    private float ssminAdd;
    private float ssmaxAdd;
    private float ssAdd;
    private float wfsAdd;

    private int hp;


    private void Start()
    {
        EM = World.DefaultGameObjectInjectionWorld.EntityManager;
        enemy = GetComponent<VisualEffect>();
        if (EM.HasComponent<TakeDamageData>(ent))
        {
            damagable = true;
        }
    }

    private void FixedUpdate()
    {
        if (EM.Exists(ent))
        {
            transform.position = EM.GetComponentData<Translation>(ent).Value;
            if (damagable)
            {
                if(EM.GetComponentData<Hp>(ent).Value == 2 && hp != 2)
                {                    
                    StartCoroutine("Pulse");
                    hp = 2;
                }
                if (EM.GetComponentData<Hp>(ent).Value == 1 && hp != 1)
                {
                    StartCoroutine("Pulse");
                    hp = 1;
                }
            }
        }
        else
        {
            if (!damagable)
            {
                Destroy(gameObject);
            }

        }

    }

    IEnumerator Pulse()
    {
        enemy.SendEvent("Strike");
        enemy.SetFloat("SparksSizeMultiplier", enemy.GetFloat("SparksSizeMultiplier") + 2.5f + ssmultAdd);
        enemy.SetFloat("TrailsSphereSizeMult", enemy.GetFloat("TrailsSphereSizeMult") + 0.7f);
        enemy.SetFloat("SparksSpawnRate", enemy.GetFloat("SparksSpawnRate") + 700 + ssrAdd);
        enemy.SetFloat("SparksSizeMinimum", enemy.GetFloat("SparksSizeMinimum") + 0.015f + ssminAdd);
        enemy.SetFloat("SparksSizeMaximum", enemy.GetFloat("SparksSizeMaximum") + 0.02f + ssmaxAdd);
        enemy.SetFloat("StripSize", enemy.GetFloat("StripSize") + 0.7f + ssAdd);
        yield return new WaitForSeconds(0.2f + wfsAdd);
        enemy.SetFloat("SparksSizeMultiplier", enemy.GetFloat("SparksSizeMultiplier") - 2.4f - ssmultAdd);
        enemy.SetFloat("TrailsSphereSizeMult", enemy.GetFloat("TrailsSphereSizeMult") - 0.35f);

        ssmultAdd = 0.5f;
        ssrAdd = 6500;
        ssminAdd = 0.01f;
        ssmaxAdd = 0.01f;
        ssAdd = 1f;
        wfsAdd = 0.05f;
    }
}
