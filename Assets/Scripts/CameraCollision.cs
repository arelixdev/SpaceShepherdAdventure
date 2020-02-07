using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDist = 3.0f;
    public float maxDist = 5.5f;
    public float smooth = 10f;
    private Vector3 dollyDir;

    private float desiredDistance;

    public int zoomRate = 40;

    private void Awake()
    {
        dollyDir = transform.localPosition.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        desiredDistance = Mathf.Clamp(desiredDistance, minDist, maxDist);

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * desiredDistance, Time.deltaTime * smooth);
    }
}
