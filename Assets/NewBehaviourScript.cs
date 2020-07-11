using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class NewBehaviourScript : MonoBehaviour
{
    public bool f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(f)
        GetComponent<VisualEffect>().SendEvent("OnPlay");
    }
    
}
