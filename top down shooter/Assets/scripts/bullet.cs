using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    float time = 0;
    public float damage = 25f;
	// Start is called before the first frame update
	void OnTriggerEnter(Collider coll)
	{
        if (coll.gameObject.layer != 9)
        {
            if (coll.gameObject.layer == 8)
            {
                coll.GetComponent<enemy>().TakeDamage(damage);
               
            }
            Debug.Log("Time of bullet:" + time);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        time += Time.deltaTime;
    }
}
