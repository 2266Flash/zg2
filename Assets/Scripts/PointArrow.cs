using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class PointArrow : MonoBehaviour
{
    public Vector3 targetPosition;
    public bool enabled;
    private SpriteRenderer renderer;
    private RectTransform pointerRectTransform;
    public GameObject parent;
    void Start()
    {
        enabled = false;
        renderer = gameObject.GetComponent<SpriteRenderer>();
        pointerRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enabled)
        {
            renderer.enabled = true;
        }
        else
        {
            renderer.enabled = false;
        }


        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        parent.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, angle+270);
    }
}
