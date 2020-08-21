using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class control : MonoBehaviour
{
    private Camera camera;
    public float speedMove = 1.5f;
    Transform player;
    Rigidbody body;
    public float sensitivity = 5f; // чувствительность мыши
    Vector3 movement;
    public float DashTime = 0.5f;
    float CurrentDashTime = 0;
    public float DashSpeed = 5;
    bool DashActive = false;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        player = transform;
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    // Update is called once per frame
    public void Moving()
    {
        body.MovePosition(body.position + movement * speedMove * Time.fixedDeltaTime);
    }
    void dir()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z= Input.GetAxisRaw("Vertical");
    }
    void rotation()
    {
        Plane playerPlane = new Plane(Vector3.up, player.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - player.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(player.rotation, targetRotation,sensitivity * Time.deltaTime);
        }
    }
    void FixedUpdate()
    {
        Moving();
        
    }
    void Rush()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && DashActive==false)
        {
            Debug.Log("Dash");
            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");
            body.velocity = new Vector3(DashSpeed * hor, 0, DashSpeed * ver);
            CurrentDashTime = DashTime;
            DashActive = true;
        }
        if (DashActive)
        {
            if (CurrentDashTime>0)
            {
                CurrentDashTime -= Time.deltaTime;
            }
            else
            {
                body.velocity = new Vector3(0, 0, 0);
                DashActive = false;
            }
        }
    }
    private void Update()
    {
        dir();
        rotation();
        Rush();
    }
}
