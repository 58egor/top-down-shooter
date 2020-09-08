using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    Transform player;
    public GameObject[] gun;
    public GameObject[] DropGun;
    public GameObject[] Allguns;
    public GameObject DotSpawn;
    public int Id = 0;
    public int PistolAmmoMax = 60;
    public int SMGAmmoMax = 120;
    public int ShootgunAmmoMax = 20;
    public int PistolAmmo=60;
    public int SMGAmmo=120;
    public int ShootgunAmmo=20;
    // Start is called before the first frame update
    void Start()
    {
        player = transform;
        Change();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Id = 0;
            Change();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Id = 1;
            Change();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Id = 2;
            Change();
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Id -= 1;
            Change();
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Id += 1;
            Change();
        }
    }
    public void Change()
    {
        if (Id > 2)
        {
            Id = 0;
        }
        if (Id < 0)
        {
            Id = 2;
        }
        if (Id == 0)
        {
            Null();
            gun[0].SetActive(true);
        }
        if (Id == 1)
        {
            Null();
            gun[1].SetActive(true);
        }
        if (Id == 2)
        {
            Null();
            gun[2].SetActive(true);
        }
    }
    void Null()
    {
        gun[0].SetActive(false);
        gun[1].SetActive(false);
        gun[2].SetActive(false);
    }
}
