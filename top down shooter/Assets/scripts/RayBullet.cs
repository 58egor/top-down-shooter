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

    public Rigidbody bullet;
    Rigidbody bulletInstance;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Создан");
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            if (time == 0)
            {
                bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity) as Rigidbody;//создаем префаб пули на сцене
                bulletInstance.velocity = transform.forward * speed;//заставляем пулю лететь
            }
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out hit))
            {
                distance = hit.distance;
                time += Time.deltaTime;
                if (distance < time * speed)
                {
                    if (hit.collider.gameObject.layer == 8)
                    {
                        hit.collider.GetComponent<enemy>().TakeDamage(25);
                    }
                    if (hit.collider.gameObject.layer != 9)
                    {
                        Debug.Log("Time:" + time);
                        Debug.Log("Удаляюсь");
                        Destroy(bulletInstance.gameObject);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
