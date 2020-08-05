using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camer : MonoBehaviour
{
    public float rangeY = 20;
    public float rangeX = 0;
    public float rangeZ = 0;
    public Rigidbody body;
    Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = body.position;
        pos.y += rangeY;
        pos.x += rangeX;
        pos.z += rangeZ;
        camera.position = pos;
    }
}
