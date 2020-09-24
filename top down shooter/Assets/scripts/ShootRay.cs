using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShootRay : MonoBehaviour
{
    //общие параметры пули
    [Header("Параметры пули:")]
    public float speed = 20;//скорость пули
    public float damage = 25;//урон пули
    public float timeout = 0.2f;//задержка между выстрелами
    public int targets = 3;//количество целей перед уничтожением пули
    private float curTimeout;
    [Header("Объекты появляющиеся при выстреле:")]
    public Transform gunPoint;//точка появления пули
    public Transform bullet;//точка отвечающая за логику пули
    public Rigidbody bul;//сама пуля
                         //параметры для смг
    [Header("Параметры для разброса пули при задержки кнопки выстрела:")]
    public int number = 5;//после какой пули увеличивается разброс
    public float degree = 1;//на сколько градусов изменяется разброс
    public int max = 10;//максимальое значение
    int chetminus = 5;
    int chetplus = 5;
    float razbros = 0;
    //параметры для дробовика
    [Header("Параметры разброса пули при выстреле+количество пуль:")]
    public float ShootgunRazbros = 10;//растояние между пулями
    public int bullets = 4;//количество пуль
    public bool ReloadOne=false;//заряжать по 1 патрону или нет
    //информация о патронах
    [Header("Информация о патронах:")]
    int holder;//текущее оличество патрон в обоиме
    public int oboima = 6;//максимальное количество патрнов в обоиме
    public float ReloadTime = 0.5f;//максимальное время перезарядки
    float rt = 0;//время перезарядки
    bool ReloadActive = false;//информация о том что активна перезарядка или нет
    [Header("Объекты для интерфейса:")]
    public Texture image;//вывод фото
    public RawImage Canvasimage;//доступ к юи
    public Text text;//текст
    public Slider slider;
    int UsedAmmo;//количество использованных патронов
    ChangeGun AmmoInfo;//доступ к масимальному количеству патронов
                       //используемы тип патронов
    [Header("Тип патронов:")]
    public bool PistolAmmo=false;
    public bool SMGAmmo = false;
    public bool ShootgunAmmo = false;
    bool NoAmmo = false;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = ReloadTime;
        slider.value = 0;
        chetminus = chetplus = number;
        AmmoInfo = transform.GetComponentInParent<ChangeGun>();
        rt = ReloadTime;
        holder = oboima;
        Canvasimage.texture = image;
        if (PistolAmmo)
        {
            text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
        }
        if (SMGAmmo)
        {
            text.text = holder.ToString() + "/" + AmmoInfo.SMGAmmo.ToString();
        }
        if (ShootgunAmmo)
        {
            text.text = holder.ToString() + "/" + AmmoInfo.ShootgunAmmo.ToString();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (PistolAmmo)
            {
                if (holder != oboima && AmmoInfo.PistolAmmo != 0)
                {
                    UsedAmmo = oboima - holder;
                    text.text = holder.ToString();
                    Debug.Log("Активирую перезарядку2");
                    ReloadActive = true;
                    slider.value = 0;
                    slider.gameObject.SetActive(true);
                }
            }
            if (SMGAmmo)
            {
                if (holder != oboima && AmmoInfo.SMGAmmo != 0)
                {
                    UsedAmmo = oboima - holder;
                    text.text = holder.ToString();
                    Debug.Log("Активирую перезарядку2");
                    ReloadActive = true;
                    slider.value = 0;
                    slider.gameObject.SetActive(true);
                }
            }
            if (ShootgunAmmo)
            {
                if (holder != oboima && AmmoInfo.ShootgunAmmo != 0)
                {
                    UsedAmmo = oboima - holder;
                    text.text = holder.ToString();
                    Debug.Log("Активирую перезарядку2");
                    ReloadActive = true;
                    slider.value = 0;
                    slider.gameObject.SetActive(true);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (PistolAmmo)
        {
            text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
        }
        if (SMGAmmo)
        {
            text.text = holder.ToString() + "/" + AmmoInfo.SMGAmmo.ToString();
        }
        if (ShootgunAmmo)
        {
            text.text = holder.ToString() + "/" + AmmoInfo.ShootgunAmmo.ToString();
        }
        ChekAmmo();
        if (NoAmmo != true || holder != 0)
        {
            if(ReloadOne==true && Input.GetMouseButton(0))
            {
                Debug.Log("Деактивирую перезарядку");
                ReloadActive = false;
                slider.gameObject.SetActive(false);
            }
            if (Input.GetMouseButton(0) && ReloadActive == false)
            {
                if (holder != 0)
                {
                    curTimeout += Time.deltaTime;
                    if (curTimeout > timeout)
                    {
                        holder--;
                        text.text = holder.ToString() + "/" + AmmoInfo.PistolAmmo.ToString();
                        Transform info;
                        Vector3 rot = transform.parent.gameObject.transform.rotation.eulerAngles;//получаем занчения поворота
                        float rand = Random.Range(-razbros, razbros);//рандомим угол отклонения
                        rot.y += rand;
                        info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));//создаем элемент отвечающий за логику пули
                        info.GetComponent<RayBullet>().damage = damage;//передаем нужные параметры урон
                        info.GetComponent<RayBullet>().speed = speed;//скорость
                        info.GetComponent<RayBullet>().bullet = bul;//какую пулю использовать
                        info.GetComponent<RayBullet>().targets = targets;//количество целей
                        info.GetComponent<RayBullet>().ready = true;//указываем что данные были отправлены
                        curTimeout = 0;
                        for (int i = 0; i < (bullets / 2); i++)//если надо что бы несколько пулей были в разные стороны
                        {
                            rot = transform.parent.gameObject.transform.rotation.eulerAngles;//изменяем угол пули в соответсвтие с параметром
                            rot.y -= ShootgunRazbros * (i + 1);
                            info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));
                            info.GetComponent<RayBullet>().damage = damage;
                            info.GetComponent<RayBullet>().speed = speed;
                            info.GetComponent<RayBullet>().bullet = bul;
                            info.GetComponent<RayBullet>().targets = targets;
                            info.GetComponent<RayBullet>().ready = true;
                            Debug.Log("Префаб создан");
                        }
                        for (int i = 0; i < (bullets / 2); i++)//если надо что бы несколько пулей были в разные стороны
                        {
                            rot = transform.parent.gameObject.transform.rotation.eulerAngles;
                            rot.y += ShootgunRazbros * (i + 1);
                            info = Instantiate(bullet, gunPoint.position, Quaternion.Euler(rot.x, rot.y, rot.z));
                            info.GetComponent<RayBullet>().damage = damage;
                            info.GetComponent<RayBullet>().speed = speed;
                            info.GetComponent<RayBullet>().bullet = bul;
                            info.GetComponent<RayBullet>().targets = targets;
                            info.GetComponent<RayBullet>().ready = true;
                            Debug.Log("Префаб создан");
                        }
                        chetplus--;//после определенного количества пуль начинается разброс
                        chetminus = number;
                    }
                }
                else
                {
                    UsedAmmo = oboima - holder;
                    ReloadActive = true;
                    slider.value = 0;
                    slider.gameObject.SetActive(true);
                }
            }
            else
            {
                curTimeout = timeout + 1;
                chetminus--;//если не стреляем определенное время то разброс уменьшается
                chetplus = number;
            }
            if (chetplus == 0)//определенное количество пуль было выпущено
            {
                chetplus = number;//востанавливаем счетчик
                if (razbros < max)//если не больше максимума то увеличиваем область разброса
                {
                    razbros += degree;
                }
            }
            if (chetminus == 0)
            {
                chetminus = number;
                if (razbros > 0)
                {
                    razbros -= degree;
                }
            }
            if (ReloadActive)
            {
                Reload();
            }
        }
    }
    void Reload()
    {
        if (rt > 0)
        {
            rt -= Time.deltaTime;
            Debug.Log("Перезарядка:" + rt);
            slider.value = ReloadTime-rt;
        }
        else
        {
            slider.value = ReloadTime - rt;
            if (ReloadOne == false)
            {
                ReloadActive = false;
                rt = ReloadTime;
                if (PistolAmmo)
                {
                    int patron = AmmoInfo.PistolAmmo - oboima;
                    if (patron < 0)
                    {
                        holder += AmmoInfo.PistolAmmo;
                        AmmoInfo.PistolAmmo = 0;
                    }
                    else
                    {
                        holder += UsedAmmo;
                        AmmoInfo.PistolAmmo = AmmoInfo.PistolAmmo - UsedAmmo;
                    }
                    slider.gameObject.SetActive(false);
                }
                if (SMGAmmo)
                {
                    int patron = AmmoInfo.SMGAmmo - oboima;
                    if (patron < 0)
                    {
                        holder += AmmoInfo.SMGAmmo;
                        AmmoInfo.SMGAmmo = 0;
                    }
                    else
                    {
                        Debug.Log(UsedAmmo);
                        holder += UsedAmmo;
                        AmmoInfo.SMGAmmo = AmmoInfo.SMGAmmo - UsedAmmo;
                    }
                    slider.gameObject.SetActive(false);
                }
                if (ShootgunAmmo)
                {
                    int patron = AmmoInfo.ShootgunAmmo - oboima;
                    if (patron < 0)
                    {
                        holder += AmmoInfo.ShootgunAmmo;
                        AmmoInfo.ShootgunAmmo = 0;
                    }
                    else
                    {
                        Debug.Log(UsedAmmo);
                        holder += UsedAmmo;
                        AmmoInfo.ShootgunAmmo = AmmoInfo.ShootgunAmmo - UsedAmmo;
                    }
                    slider.gameObject.SetActive(false);
                }

            }
            else
            {
                rt = ReloadTime;
                holder++;
                AmmoInfo.ShootgunAmmo--;
                if (holder == oboima || AmmoInfo.ShootgunAmmo == 0)
                {
                    Debug.Log("Перезарядка закончилась");
                    ReloadActive = false;
                    slider.gameObject.SetActive(false);
                }
            }
        }
    }
    void ChekAmmo()
    {
        if (PistolAmmo)
        {
            if (AmmoInfo.PistolAmmo == 0)
                NoAmmo = true;
            else
                NoAmmo = false;
        }
        if (SMGAmmo)
        {
            if (AmmoInfo.SMGAmmo == 0)
                NoAmmo = true;
            else
                NoAmmo = false;
        }
        if (ShootgunAmmo)
        {
            if (AmmoInfo.ShootgunAmmo == 0)
                NoAmmo = true;
            else
                NoAmmo = false;
        }
    }
    void OnDisable()
    {
       if(holder!=0) ReloadActive = false;
        rt = ReloadTime;
    }
    private void OnEnable()
    {
        slider.gameObject.SetActive(false);
        slider.maxValue = ReloadTime;
        slider.value = 0;
        Canvasimage.texture = image;
        text.text = holder.ToString();
    }
}
