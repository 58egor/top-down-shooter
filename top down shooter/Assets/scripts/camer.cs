using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camer : MonoBehaviour
{
    public float rangeY = 20;
    public float rangeX = 0;
    public float rangeZ = 0;
    public Rigidbody body;
    Vector3 mouse;
    Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform;
        Vector3 pos = body.position;
        pos.y += rangeY;
        camera.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = body.position;
        pos.y += rangeY;
        camera.position = pos;
        //mouse = Input.mousePosition;
        //Vector3 pos =Camera.main.ScreenToWorldPoint(body.position);
        //Vector3 mousePos= Camera.main.ScreenToWorldPoint(mouse);
        //Vector3 center = (mousePos + pos) / 2;
        ////camera.position = pos;
        //camera.position = Vector3.Lerp(camera.position,new Vector3(center.x,body.position.y+rangeY,center.z),0.25f);
    }
}
