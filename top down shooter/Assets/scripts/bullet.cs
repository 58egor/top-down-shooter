using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
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
            Destroy(gameObject);
        }
    }
}
