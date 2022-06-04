using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Shake : MonoBehaviour
{
    private Vector2 startpos;

    public void Start()
    {
        startpos = gameObject.transform.position;
    }
    public void Update()
    {
        moveShake();
    }
    
    public void moveShake()
    {
        Random r = new Random();
        int dir = r.Next(0, 7);
        float no = (float) r.NextDouble();
        if (dir == 1)
        {
            if (!(gameObject.transform.position.x - startpos.x > 20))
            {
                gameObject.transform.Translate(new Vector3(no, 0));
            }
        }
        else if (dir == 2)
        {
            if (!(gameObject.transform.position.y - startpos.y > 20))
            {
                gameObject.transform.Translate(new Vector3(0, no));
            }
        }
        else if (dir == 3)
        {
            if (!(startpos.y - gameObject.transform.position.y > 20))
            {
                gameObject.transform.Translate(new Vector3(0, -no));
            }
        }
        else if (dir == 4)
        {
            if (!(startpos.x - gameObject.transform.position.x > 20))
            {
                gameObject.transform.Translate(new Vector3(-no, 0));
            }
        }
    }
}
