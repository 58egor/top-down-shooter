using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChRldSMG : MonoBehaviour
{
    public string Name = "UMP";
    public int AddAmmo = 24;
    bool ready = false;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
            Debug.Log("Заряжаю");
            int length = player.GetComponent<ChangeGun>().gun.Length;
            for (int i = 0; i < length; i++)
            {
                if (player.GetComponent<ChangeGun>().gun[i].GetComponent<GunInfo>().Name == Name)
                {
                    Debug.Log("Заряжаю");
                    ChangeGun info = player.GetComponent<ChangeGun>();
                    if (info.SMGAmmo != info.SMGAmmoMax)
                    {
                        if (info.SMGAmmo + AddAmmo > info.SMGAmmoMax)
                        {
                            info.SMGAmmo = info.SMGAmmoMax;
                        }
                        else
                        {
                            info.SMGAmmo += AddAmmo;
                        }
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
