using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChRldPistol : MonoBehaviour
{
    public string Name = "Revolver";
    public int AddAmmo = 24;
    bool ready = false;
    public GameObject player;
    public int GunId = 3;
    public float LongOfChageDefault=3f;
    float LongOfChage;
    bool Have = false;
    // Start is called before the first frame update
    void Start()
    {
        LongOfChage = LongOfChageDefault;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            player = other.gameObject;
            ready = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ready = false;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            Change();
            Reload();
        }
    }
    void Change()
    {
        int length = player.GetComponent<ChangeGun>().gun.Length;
            for (int i = 0; i < length; i++)
            {
                if (player.GetComponent<ChangeGun>().gun[i].GetComponent<GunInfo>().Name == Name)
                {
                    Have = true;
                }
            }
        //Debug.Log(Have);
        if(Have==false && Input.GetKeyDown(KeyCode.E))
        {
                int i = player.GetComponent<ChangeGun>().Id;
                ChangeGun info = player.GetComponent<ChangeGun>();
                info.gun[i].SetActive(false);
            info.gun[i].GetComponent<PistolRay>().removed();
                Instantiate(info.DropGun[info.gun[i].GetComponent<GunInfo>().Id], info.DotSpawn.transform.position, info.DropGun[info.gun[i].GetComponent<GunInfo>().Id].transform.rotation); 
                info.gun[i] = info.Allguns[GunId];
                player.GetComponent<ChangeGun>().Change();
                Destroy(gameObject);
                Debug.Log("done");
        }
        else
        {
            LongOfChage = LongOfChageDefault;
        }
    }
    void Reload()
    {
        //Debug.Log("Заряжаю");
                ChangeGun info = player.GetComponent<ChangeGun>();
                if (info.PistolAmmo != info.PistolAmmoMax)
                {
                    if (info.PistolAmmo + AddAmmo > info.PistolAmmoMax)
                    {
                        info.PistolAmmo = info.PistolAmmoMax;
                    }
                    else
                    {
                        info.PistolAmmo += AddAmmo;
                    }
                    AddAmmo = 0;
                    //Destroy(gameObject);
        }
    }
}
