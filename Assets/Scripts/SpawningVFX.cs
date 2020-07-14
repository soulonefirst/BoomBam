using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


public class SpawningVFX : MonoBehaviour
{

    public static List<Queue<GameObject>> pool;

    public static void Initialization()
    {

        GameObject[] pref = Resources.LoadAll<GameObject>("Prefabs/VFXGameObjects");

        pool = new List<Queue<GameObject>>();

        foreach (GameObject gameObject in pref)
        {
            var q = new Queue<GameObject>();
            for (int i = 0; i <= 30; i++)
            {
                var gO = Instantiate(gameObject);
                gO.SetActive(false);
                q.Enqueue(gO);
            }
            pool.Add(q);
        }

    }

    public static void VFXSpawn(Entity entity, int poolId)
    {
        GameObject gameObject;
        gameObject = pool[poolId].Dequeue();
        gameObject.SetActive(true);
        gameObject.GetComponent<VFXTransformRelation>().ent = entity;
        gameObject.GetComponent<VFXTransformRelation>().poolId = poolId;
    }

}
