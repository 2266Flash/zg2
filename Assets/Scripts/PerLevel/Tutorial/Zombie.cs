using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = GameObject.Find("Player").GetComponent<PlayerManager>();
        Vector3 toPosition = pm.gameObject.transform.position;
        Vector3 fromPosition = transform.position;
        Vector3 goTo = Vector3.MoveTowards(fromPosition, toPosition, Time.deltaTime*2);
        transform.position = goTo;

    }
    
}
