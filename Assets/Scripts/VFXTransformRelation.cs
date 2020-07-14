using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.VFX;
using System.Collections;


public class VFXTransformRelation : MonoBehaviour
{
    public Entity ent;
    public int poolId;
    private bool damagable;
    EntityManager EM;
    VisualEffect visualEffect;
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
        visualEffect = GetComponent<VisualEffect>();
        if (EM.HasComponent<TakeDamageData>(ent))
        {
            damagable = true;
            hp = EM.GetComponentData<Hp>(ent).Value;
        }
    }

    private void FixedUpdate()
    {
        if (EM.Exists(ent))
        {
            transform.position = EM.GetComponentData<Translation>(ent).Value;
            if (damagable)
            {
                if (EM.GetComponentData<Hp>(ent).Value != hp)
                {
                    StartCoroutine("Pulse");
                    hp = EM.GetComponentData<Hp>(ent).Value;
                }
            }
        }
        else
        {
            AddToPool();
        }

    }

    private void AddToPool()
    {
        SpawningVFX.pool[poolId].Enqueue(gameObject);

        //
        gameObject.SetActive(false);
    }
    IEnumerator Pulse()
    {

         visualEffect.SendEvent("Strike");
         visualEffect.SetFloat("SparksSizeMultiplier",  visualEffect.GetFloat("SparksSizeMultiplier") + 2.5f + ssmultAdd);
         visualEffect.SetFloat("TrailsSphereSizeMult",  visualEffect.GetFloat("TrailsSphereSizeMult") + 0.7f);
         visualEffect.SetFloat("SparksSpawnRate",  visualEffect.GetFloat("SparksSpawnRate") + 700 + ssrAdd);
         visualEffect.SetFloat("SparksSizeMinimum",  visualEffect.GetFloat("SparksSizeMinimum") + 0.015f + ssminAdd);
         visualEffect.SetFloat("SparksSizeMaximum",  visualEffect.GetFloat("SparksSizeMaximum") + 0.02f + ssmaxAdd);
         visualEffect.SetFloat("StripSize",  visualEffect.GetFloat("StripSize") + 0.7f + ssAdd);
        yield return new WaitForSeconds(0.2f + wfsAdd);
         visualEffect.SetFloat("SparksSizeMultiplier",  visualEffect.GetFloat("SparksSizeMultiplier") - 2.4f - ssmultAdd);
         visualEffect.SetFloat("TrailsSphereSizeMult",  visualEffect.GetFloat("TrailsSphereSizeMult") - 0.35f);

        ssmultAdd = 0.5f;
        ssrAdd = 6500;
        ssminAdd = 0.01f;
        ssmaxAdd = 0.01f;
        ssAdd = 1f;
        wfsAdd = 0.05f;
    }
}
