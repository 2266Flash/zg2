using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    public Canvas good;
    public Canvas bad;
    void Start()
    {
        if (globalVars.cruelty > 0)
        {
            bad.enabled = true;
            good.enabled = false;
        }
        else
        {
            bad.enabled = false;
            good.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
