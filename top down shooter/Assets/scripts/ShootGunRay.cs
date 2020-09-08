using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
	int holder;
	public int oboima = 30;
	public float ReloadTime = 0.5f;
	float rt = 0;
	bool ReloadActive = false;
	public Texture image;
	public RawImage Canvasimage;
	public Text text;
	ChangeGun AmmoInfo;
	// Start is called before the first frame update
	void Start()
    {
		AmmoInfo = transform.GetComponentInParent<ChangeGun>();
		holder = oboima;
		rt = ReloadTime;
		Canvasimage.texture = image;
		text.text = holder.ToString();
    }

    // Update is called once per frame
    private void Update()
    {
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (holder != oboima && AmmoInfo.ShootgunAmmo != 0)
			{
				Debug.Log("Активирую перезарядку2");
				ReloadActive = true;

			}
		}
	}
    void FixedUpdate()
    {
		text.text = holder.ToString() + "/" + AmmoInfo.ShootgunAmmo.ToString();
		if (AmmoInfo.ShootgunAmmo!= 0 || holder != 0)
		{
			if (Input.GetMouseButton(0))
			{
				if (holder != 0)
				{
					curTimeout += Time.deltaTime;
					if (curTimeout > timeout)
					{
						holder--;
						text.text = holder.ToString();
						ReloadActive = false;
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
							rot.y -= razbros * (i + 1);
							info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));
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
					ReloadActive = true;
				}
			}
			else
			{
				curTimeout = timeout + 1;
			}

			if (ReloadActive)
			{
				Reload();
			}
			else
			{
				rt = ReloadTime;
			}
		}
	}
	void Reload()
	{
		if (rt > 0 && holder!=oboima)
		{
			rt -= Time.deltaTime;
			Debug.Log("Перезарядка:" + rt);
		}
		else
		{
			rt = ReloadTime;
			holder++;
			AmmoInfo.ShootgunAmmo--;
		}
        if (holder == oboima || AmmoInfo.ShootgunAmmo== 0)
        {
			Debug.Log("Перезарядка закончилась");
			ReloadActive = false;
        }
	}
	void OnDisable()
	{
		rt = ReloadTime;
	}
	private void OnEnable()
	{
		Canvasimage.texture = image;
		text.text = holder.ToString();
	}
}
