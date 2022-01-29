using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;

    private float initialZ;

    void Start()
    {
        initialZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraPosition = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, initialZ);
        transform.position = cameraPosition;
    }
}
