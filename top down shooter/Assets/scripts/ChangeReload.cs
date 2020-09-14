using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeReload : MonoBehaviour
{
    [Header("Имя оружия:")]
    public string Name = "Revolver2";
    [Header("Информация о патронах:")]
    public int Ammo = 48;
    public int AddAmmo = 24;
    bool ready = false;
    [Header("Игрок:")]
    public GameObject player;
    [Header("ай ди пушки из скрипта change gun:")]
    public int GunId = 3;
    [Header("длительность перезарядки:")]
    public float LongOfChageDefault = 0f;
    float LongOfChage;
    bool Have = false;
    [Header("тип патронов:")]
    public  bool PistolAmmo=false;
    public bool SMGAmmo = false;
    public bool ShootgunAmmo = false;
    // Start is called before the first frame update
    void Start()
    {
        LongOfChage = LongOfChageDefault;
    }
    private void OnTriggerEnter(Collider other)
    {//проверяем зашел ли игрок врадиус тригерра,если да то разрешить добавление патронов и смену оружия
        if (other.tag == "Player")
        {
            player = other.gameObject;
            ready = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {//если игрок вышел то запрет
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
    //функция отвечающа за смену оружия
    void Change()
    {
        //проверяем есть ли пушка с таким же именем
        int length = player.GetComponent<ChangeGun>().gun.Length;
        for (int i = 0; i < length; i++)
        {
            if (player.GetComponent<ChangeGun>().gun[i].GetComponent<GunInfo>().Name == Name)
            {
                Have = true;
                break;
            }
            else Have = false;
        }
        Debug.Log(Have);
        //если нет и была нажата кнопка е то меняем оружие
        if (Have == false && Input.GetKeyDown(KeyCode.E))
        {
            int i = player.GetComponent<ChangeGun>().Id;//узнаем место в массиве которое должны поменять
            ChangeGun info = player.GetComponent<ChangeGun>();
            info.gun[i].SetActive(false);//деактивируем оружие
           // info.gun[i].GetComponent<PistolRay>().removed();
            Instantiate(info.DropGun[info.gun[i].GetComponent<GunInfo>().Id], info.DotSpawn.transform.position, info.DropGun[info.gun[i].GetComponent<GunInfo>().Id].transform.rotation);//спавним пушку которую держали
            info.gun[i] = info.Allguns[GunId];//записываем в массив новое оружие
            player.GetComponent<ChangeGun>().Change();
            Destroy(gameObject);
            Debug.Log("done");
        }
        else
        {
            LongOfChage = LongOfChageDefault;
        }
    }
    //функция добавления патронов
    void Reload()
    {
        ChangeGun info = player.GetComponent<ChangeGun>();
        //если пистолет
        if (PistolAmmo)
        {
            //если патронов не максимум то добавляем
            if (info.PistolAmmo != info.PistolAmmoMax)
            {//если в результате будет максимум то просто привавнимаем к нему
                if (info.PistolAmmo + AddAmmo > info.PistolAmmoMax)
                {
                    info.PistolAmmo = info.PistolAmmoMax;
                }
                else
                {//иначе добалвяем
                    info.PistolAmmo += AddAmmo;
                }
                AddAmmo = 0;
                //Destroy(gameObject);
            }
        }
        //если пп/автомат
        if (SMGAmmo)
        {
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
                AddAmmo = 0;
                //Destroy(gameObject);
            }
        }
        //если дробовик
        if (ShootgunAmmo)
        {
            if (info.ShootgunAmmo != info.ShootgunAmmoMax)
            {
                if (info.ShootgunAmmo + AddAmmo > info.ShootgunAmmoMax)
                {
                    info.ShootgunAmmo = info.ShootgunAmmoMax;
                }
                else
                {
                    info.ShootgunAmmo += AddAmmo;
                }
                AddAmmo = 0;
                //Destroy(gameObject);
            }
        }
    }
}
