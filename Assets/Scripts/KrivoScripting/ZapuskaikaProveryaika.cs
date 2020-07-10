using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ZapuskaikaProveryaika : MonoBehaviour
{
    VisualEffect enemy;
    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<VisualEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            StartCoroutine("Pulse");
            hit = false;
        }
    }

    IEnumerator Pulse()
    {
        enemy.SendEvent("Strike");
        enemy.SetFloat("SparksSizeMultiplier", enemy.GetFloat("SparksSizeMultiplier") + 2);
        enemy.SetFloat("SparksSpawnRate", enemy.GetFloat("SparksSpawnRate") + 10000);
        yield return new WaitForSeconds(0.13f);
        enemy.SetFloat("SparksSizeMultiplier", enemy.GetFloat("SparksSizeMultiplier") -1.9f);


    }
}
