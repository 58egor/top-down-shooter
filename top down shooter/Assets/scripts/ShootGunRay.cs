using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunRay : MonoBehaviour
{
    public float speed = 20;
    public float damage = 25;
    public float timeout = 0.2f;
    public int targets = 3;
    private float curTimeout;
    public Transform gunPoint;
    public Transform bullet;
    public Rigidbody bul;
	public float razbros=10;
	public int bullets = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButton(0))
		{
			curTimeout += Time.deltaTime;
			if (curTimeout > timeout)
			{
				Transform info;
				info = Instantiate(bullet, gunPoint.position, transform.parent.gameObject.transform.rotation);
				info.GetComponent<RayBullet>().damage = damage;
				info.GetComponent<RayBullet>().speed = speed;
				info.GetComponent<RayBullet>().bullet = bul;
				info.GetComponent<RayBullet>().targets = targets;
				info.GetComponent<RayBullet>().ready = true;
				for (int i = 0; i < (bullets / 2); i++)
				{
					Vector3 rot = transform.parent.gameObject.transform.rotation.eulerAngles;
					rot.y -= razbros*(i+1);
					info = Instantiate(bullet, gunPoint.position,Quaternion.Euler(rot.x, rot.y, rot.z));
					info.GetComponent<RayBullet>().damage = damage;
					info.GetComponent<RayBullet>().speed = speed;
					info.GetComponent<RayBullet>().bullet = bul;
					info.GetComponent<RayBullet>().targets = targets;
					info.GetComponent<RayBullet>().ready = true;
					Debug.Log("Префаб создан");
				}
				for (int i = 0; i < (bullets / 2); i++)
				{
					Vector3 rot = transform.parent.gameObject.transform.rotation.eulerAngles;
					rot.y += razbros * (i + 1);
					info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));
					info.GetComponent<RayBullet>().damage = damage;
					info.GetComponent<RayBullet>().speed = speed;
					info.GetComponent<RayBullet>().bullet = bul;
					info.GetComponent<RayBullet>().targets = targets;
					info.GetComponent<RayBullet>().ready = true;
					Debug.Log("Префаб создан");
				}
				curTimeout = 0;
			}

		}
		else
		{
			curTimeout = timeout + 1;
		}
	}
}
