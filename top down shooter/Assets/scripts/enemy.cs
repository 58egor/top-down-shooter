using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody body;
    public float HP=100;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        HP -= damage;
        if (HP < 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Debug.Log("Не бей.Я умер");
        Destroy(this.gameObject);
    }
}
