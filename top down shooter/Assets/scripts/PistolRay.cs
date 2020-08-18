using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRay : MonoBehaviour
{
	public float speed = 20;
    public float damage = 25;
    public float timeout = 0.2f;
	public int targets = 3;
    private float curTimeout;
    public Transform gunPoint;
	public Transform bullet;
	public Rigidbody bul;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

				if (Input.GetMouseButton(1))
				{
							curTimeout += Time.deltaTime;
							if (curTimeout > timeout)
							{
				Transform info;
				info=Instantiate(bullet, gunPoint.position, transform.rotation);
				info.GetComponent<RayBullet>().damage = damage;
				info.GetComponent<RayBullet>().speed = speed;
				info.GetComponent<RayBullet>().bullet = bul;
				info.GetComponent<RayBullet>().targets = targets;
				info.GetComponent<RayBullet>().ready = true;
				Debug.Log("Префаб создан");
						       curTimeout = 0;
					         }
	
				}
				else
				{
					curTimeout = timeout + 1;
				}
    }
}
