using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5.0f;

    Vector3 offset;

    void Start()
    {
        if (target != null)
            offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target == null) return;

        Vector3 targetCameraPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
    }
}
