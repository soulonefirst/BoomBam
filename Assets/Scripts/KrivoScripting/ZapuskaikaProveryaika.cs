using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class ZapuskaikaProveryaika : MonoBehaviour
{
    VisualEffect enemy;
    public bool hit;
    private float ssmultAdd = 0;
    private float ssrAdd = 0;
    private float ssminAdd = 0;
    private float ssmaxAdd = 0;
    private float ssAdd = 0;
    private float wfsAdd = 0;

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
