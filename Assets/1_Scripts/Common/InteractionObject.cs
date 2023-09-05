using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Outline))]
public class InteractionObject : MonoBehaviour
{
    private Outline outline;
    private float saveWidth;

    public void Start()
    {
        outline = GetComponent<Outline>();
        saveWidth = outline.OutlineWidth;
        outline.OutlineWidth = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.LogError("조사 오브젝트 클릭");
    }

    //public void OnMouseEnter()
    //{
    //    outline.OutlineWidth = saveWidth;
    //}

    //public void OnMouseExit()
    //{
    //    outline.OutlineWidth = 0;
    //}

    //public void OnMouseUp()
    //{
    //    Debug.LogError("조사 오브젝트 클릭");
    //}
}