﻿using System.Collections;
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
	public int number=5;//после какой пули увеличивается разброс
	public float degree = 1;//на сколько градусов изменяется разброс
	public int max = 10;//максимальое значение
	int chetminus = 5;
	int chetplus = 5;
	float razbros = 0;
	int holder;
	public int oboima = 30;
	public float ReloadTime = 2f;
	float rt = 0;
	bool ReloadActive = false;
	// Start is called before the first frame update
	void Start()
	{
		chetminus = chetplus = number;
		rt = ReloadTime;
		holder = oboima;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))//если пользователь нажал р то активируем перезарядку
		{
			if (holder != oboima)//проверяем что у нас не макс патронов
			{
				holder = 0;//опустошаем магазин
				Debug.Log("Активирую перезарядку2");
				ReloadActive = true;

			}
		}
	}
	// Update is called once per frame
	void FixedUpdate()
	{

		if (Input.GetMouseButton(0) && ReloadActive==false)//если нажали кнопку выстрела
		{
			if (holder != 0)//если есть патроны
			{
				curTimeout += Time.deltaTime;
				if (curTimeout > timeout)//проверяем что задержка закончилась
				{
					holder--;//вычитаем патрон
					Transform info;
					Vector3 rot = transform.parent.gameObject.transform.rotation.eulerAngles;//получаем занчения поворота
					float rand = Random.Range(-razbros, razbros);//рандомим угол отклонения
					rot.y += rand;
					info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));//создаем пустой объект отвечающий за алгоритм пули
					info.GetComponent<RayBullet>().damage = damage;//передаем нужные данные для пули
					info.GetComponent<RayBullet>().speed = speed;
					info.GetComponent<RayBullet>().bullet = bul;
					info.GetComponent<RayBullet>().targets = targets;
					info.GetComponent<RayBullet>().ready = true;
					Debug.Log("Префаб создан");
					curTimeout = 0;
					chetplus--;//после определенного количества пуль начинается разброс
					chetminus = number;
				}
			}
			else
            {
				ReloadActive = true;//если патронов 0 то перезаряжаем
            }

		}
		else
		{
			curTimeout = timeout + 1;
			chetminus--;//если не стреляем определенное время то разброс уменьшается
			chetplus = number;
		}
		if (chetplus == 0)//определенное количество пуль было выпущено
        {
			chetplus = number;//востанавливаем счетчик
			if (razbros < max)//если не больше максимума то увеличиваем область разброса
			{
				razbros += degree;
			}
        }
        if (chetminus == 0)
        {
			chetminus = number;
            if (razbros > 0)
            {
				razbros -= degree;
            }
        }
		
		if (ReloadActive)//вызываем функцию для перезарядки оружия
		{
			Reload();
		}
	}
	void Reload()//функция отвечающая за перезарядки пушки
	{
		if (rt > 0)//ждем пока таймер не закончится(в будующем чделать зависимость от анимации)
		{
			rt -= Time.deltaTime;
			Debug.Log("Перезарядка:" + rt);
		}
		else
		{
			razbros = 0;
			ReloadActive = false;
			rt = ReloadTime;
			holder = oboima;
			Debug.Log("Перезарядка закончилась");
		}
	}
	void OnDisable()
	{
		rt = ReloadTime;
	}
}

