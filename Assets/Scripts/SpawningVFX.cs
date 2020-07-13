
using UnityEngine;
using Unity.Entities;


public class SpawningVFX : MonoBehaviour
{

    private static GameObject redEnemy;

    private static GameObject greenEnemy;

    private static GameObject blueEnemy;

    private static GameObject redBullet;

    private static GameObject greenBullet;

    private static GameObject blueBullet;

    public static void Initialization()
    {

        GameObject[] pref = Resources.LoadAll<GameObject>("Prefabs/VFXGameObjects");
        blueBullet = pref[0];
        greenBullet = pref[1];
        redBullet = pref[2];
        blueEnemy = pref[3];
        greenEnemy = pref[4];
        redEnemy = pref[5];
    }

    public static void VFXSpawn(Entity entity, int prefToSpawn)
    {
        GameObject gameObject;

            switch (prefToSpawn)
            {
                case 0:
                    gameObject = Instantiate(redBullet);
                    gameObject.GetComponent<VFXTransformRelation>().ent = entity;
                    break;
                case 1:
                    gameObject = Instantiate(blueBullet);
                    gameObject.GetComponent<VFXTransformRelation>().ent = entity;
                        break;
                case 2:
                    gameObject = Instantiate(greenBullet);
                    gameObject.GetComponent<VFXTransformRelation>().ent = entity;
                        break;
                case 3:
                    gameObject = Instantiate(redEnemy);
                    gameObject.GetComponent<VFXTransformRelation>().ent = entity;
                        break;
                case 4:
                    gameObject = Instantiate(blueEnemy);
                    gameObject.GetComponent<VFXTransformRelation>().ent = entity;
                        break;
                case 5:
                    gameObject = Instantiate(greenEnemy);
                    gameObject.GetComponent<VFXTransformRelation>().ent = entity;
                        break;
            }
        
    }

}
