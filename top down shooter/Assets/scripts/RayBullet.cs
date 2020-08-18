using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public bool ready = false;
    float distance;
    float time = 0;
    public int targets=1;
    public Rigidbody bullet;
    List<Collider> names=new List<Collider>();
    Rigidbody bulletInstance;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Создан");
    }

    // Update is called once per frame
    void MultiShot()
    {
        int j = 0;
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward);
        RaycastHit copy;
        if (hit.Length != 0)
        {
            for (int i = 0; i < hit.Length - 1; i++)//сортируем полученные цели по дистанции по возрастанияю
        {
            for(int i2 = i+1; i2 < hit.Length; i2++)
            {
                if (hit[i].distance > hit[i2].distance)
                {
                    copy = hit[i];
                    hit[i] = hit[i2];
                    hit[i2] = copy;
                }

            }
        }

            if (names.Count==0)//если у нас еще не было попаданий
            {
                Debug.Log("Target:" + hit[j].collider.name);
                distance = hit[j].distance;
            }
            else//если было надо проверить имена объектов в луче с теми которые мы уже прострелили
            {
                int count = 1;//количество совпадений
                while (count != 0)//проверяем пока не найдем цель в которую не попадали
                {
                    count = 0;
                    foreach(Collider o in names)//проверяем со списком попавших целей
                    {
                        if (hit[j].collider == o)//если было совпадений то проверяем следующую цель
                        {
                            j++;
                            count++;
                            break;
                        }
                    }
                }
                distance = hit[j].distance;
            }
            time += Time.deltaTime;//увеличиваем время
            if (distance < time * speed && time * speed < distance + (speed/20))
            {
                Debug.Log("попал");
                if (hit[j].collider.gameObject.layer == 8)//8-слой врага
                {
                    Debug.Log("попал во врага");
                    //Debug.Log("Target:"+ hit[j].collider.name+",Distance{1}" + hit[j].distance+",Formula:{2}" +(time * speed));
                    hit[j].collider.GetComponent<enemy>().TakeDamage(damage);//вызываем функцию урона у врага
                    names.Add(hit[j].collider);//добавляем в коллекцию попавших целей
                    targets--;
                }
                if (hit[j].collider.gameObject.layer == 10)//10-слой препятствия
                {
                    damage = damage - 10;//если попали в препятствие то снижаем урон пули
                    names.Add(hit[j].collider);//добавляем в коллекцию попавших целей
                }
                if (hit[j].collider.gameObject.layer == 0 || targets == 0 || damage<=0)//удаялем пулю и луч если попали в слой стены или количество целей закончилось
                {
                    Debug.Log("Удаляюсь");
                    Destroy(bulletInstance.gameObject);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (time * speed > distance + speed/20)
                {
                    names.Add(hit[j].collider);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (ready)
        {
            if (time == 0)
            {
                bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as Rigidbody;//создаем префаб пули на сцене
                bulletInstance.velocity = transform.forward * speed;//заставляем пулю лететь
            }
            MultiShot();
        }
    }
}
