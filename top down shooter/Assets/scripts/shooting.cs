using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    public Rigidbody bullet;
    public Transform gunPoint;
    public int bulletSpeed = 10;
    public float timeout = 10f;
    private float curTimeout;
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
                curTimeout = 0;
                Rigidbody bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody;//создаем префаб пули на сцене
                bulletInstance.velocity = gunPoint.forward * bulletSpeed;//заставляем пулю лететь

            }
        }
        else
        {
            curTimeout = timeout + 1;
        }
    }
}
