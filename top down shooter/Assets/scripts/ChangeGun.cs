using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GetComponent<PistolRay>().enabled = false;
            GetComponent<ShootGunRay>().enabled = false;
            GetComponent<SMGRay>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GetComponent<PistolRay>().enabled = true;
            GetComponent<ShootGunRay>().enabled = false;
            GetComponent<SMGRay>().enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GetComponent<PistolRay>().enabled = false;
            GetComponent<ShootGunRay>().enabled = true;
            GetComponent<SMGRay>().enabled = false;
        }
    }
}
