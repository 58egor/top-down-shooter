using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    int holder;
	public int oboima=6;
	public float ReloadTime = 0.5f;
	float rt = 0;
	bool ReloadActive = false;
	public Texture image;
	public RawImage Canvasimage;
	public Text text;
	int UsedAmmo;
	ChangeGun AmmoInfo;
	// Start is called before the first frame update
	void Start()
    {
		AmmoInfo = transform.GetComponentInParent<ChangeGun>();
		rt = ReloadTime;
		holder = oboima;
		Canvasimage.texture = image;
		text.text = holder.ToString()+"/" + AmmoInfo.PistolAmmo.ToString();
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (holder != oboima && AmmoInfo.PistolAmmo!=0)
			{
				UsedAmmo = oboima-holder;
				text.text = holder.ToString();
				Debug.Log("Активирую перезарядку2");
				ReloadActive = true;

			}
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{
		text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
		if (AmmoInfo.PistolAmmo != 0 || holder !=0)
		{
			if (Input.GetMouseButton(0) && ReloadActive == false)
			{
				if (holder != 0)
				{
					curTimeout += Time.deltaTime;
					if (curTimeout > timeout)
					{
						holder--;
						text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
						Transform info;
						info = Instantiate(bullet, gunPoint.position, transform.parent.gameObject.transform.rotation);
						info.GetComponent<RayBullet>().damage = damage;
						info.GetComponent<RayBullet>().speed = speed;
						info.GetComponent<RayBullet>().bullet = bul;
						info.GetComponent<RayBullet>().targets = targets;
						info.GetComponent<RayBullet>().ready = true;
						Debug.Log("Патронов:" + holder);
						curTimeout = 0;
					}
				}
				else
				{
					UsedAmmo = oboima - holder;
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
		}
    }
	void Reload()
    {
        if (rt > 0)
        {
			rt -= Time.deltaTime;
			Debug.Log("Перезарядка:"+rt);
		}
        else
        {

			ReloadActive = false;
			rt = ReloadTime;
			int patron = AmmoInfo.PistolAmmo - oboima;
			if (patron < 0)
			{
				holder += AmmoInfo.PistolAmmo;
				AmmoInfo.PistolAmmo = 0;
			}
			else
			{
				holder +=UsedAmmo;
				AmmoInfo.PistolAmmo = AmmoInfo.PistolAmmo - UsedAmmo;
            }
			text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
			Debug.Log("Перезарядка закончилась");
		}
    }
	void OnDisable() 
	{
		rt = ReloadTime;
	}
    private void OnEnable()
    {
		Canvasimage.texture = image;
		text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
	}
	public void removed()
    {
		holder = oboima;
    }
}
