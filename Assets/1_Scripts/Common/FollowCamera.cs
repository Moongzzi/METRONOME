using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Transform cachedTransform;
    public Transform CameraCachedTransform;

    public float distance;
    public float wide;
    public float height;

    public float maxH, minH;

    public void Init()
    {
        target = IngameManager.Instance.playerUnit.transform;
        cachedTransform = gameObject.transform;
        CameraCachedTransform = cachedTransform.GetChild(0);

        CameraCachedTransform.localPosition = cachedTransform.forward * -1 * distance;
    }

    public void Update()
    {
        if (height >= maxH) height = maxH - 0.1f;
        else if (height <= minH) height = minH + 0.1f;

        cachedTransform.position = target.position;
        cachedTransform.rotation = Quaternion.Euler(height, wide, 0);

        CameraCachedTransform.LookAt(target);
    }
}
