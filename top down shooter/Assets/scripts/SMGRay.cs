using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMGRay : MonoBehaviour
{
	public float speed = 20;
	public float damage = 25;
	public float timeout = 0.2f;
	public int targets = 3;
	private float curTimeout;
	public Transform gunPoint;
	public Transform bullet;
	public Rigidbody bul;
	public int chetminus = 5;
	public int chetplus = 5;
	int razbros = 0;
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
				Vector3 rot= transform.rotation.eulerAngles;
				float rand= Random.Range(-razbros, razbros);
				rot.y += rand;
				Debug.Log("Изначальный поворот"+ transform.rotation.y+ " Случайное число" +rand+" Поворот точки:" + rot);
				info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x,rot.y,rot.z));
				info.GetComponent<RayBullet>().damage = damage;
				info.GetComponent<RayBullet>().speed = speed;
				info.GetComponent<RayBullet>().bullet = bul;
				info.GetComponent<RayBullet>().targets = targets;
				info.GetComponent<RayBullet>().ready = true;
				Debug.Log("Префаб создан");
				curTimeout = 0;
				chetplus--;
				chetminus = 5;
			}

		}
		else
		{
			curTimeout = timeout + 1;
			chetminus--;
			chetplus = 5;
		}
		if (chetplus == 0)
        {
			chetplus = 5;
			if (razbros != 10)
			{
				razbros += 1;
			}
        }
        if (chetminus == 0)
        {
			chetminus = 5;
            if (razbros != 0)
            {
				razbros -= 1;
            }
        }
	}
}
