using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ExBullet : MonoBehaviour
{
    [Header("固定的东西")]
    static public ExBullet instance;
    public float angle = 5;         //散射角度
    public int shoottime = 0;
    public bool ShotEnable = true;
    public string NextBullet;

    [Header("武器属性")]
    public WeaponProperties WP=new WeaponProperties();     //武器属性
    public int MultipleBullet;
    public int Level;           //伤害等级
    public bool Track;
    public string[] shoot;        //子弹发射序列


    public BulletPool BP;

    public GameObject HitImpact;

    public HitPos hitPos;
    void InitializeProperties()
    {
        MultipleBullet = WP.Multiple;
        Level = WP.DamageLevel;
        shoot = WP.shoot;

    }
    void Awake()
    {
        instance = this;
        hitPos = HitPos.instance;
        BP = BulletPool.instance;
        InitializeProperties();
    }
    private void Update()
    {
        NextBullet = shoot[shoottime % shoot.Length];
        transform.right = (hitPos.MouseFollow() - transform.position).normalized;
    }
    public void SetMultipleShot(GameObject ab)
    {
        switch (MultipleBullet)
        {
            case 1:
                break;
            case 2:
                SetMultiple2(ab);
                break;
            case 3:
                SetMultiple3(ab);
                break;
            case 4:
                SetMultiple4(ab);
                break;
            default:
                break;
        }
    }
    public void SetDamage(GameObject ab)
    {

    }

    void SetMultiple2(GameObject ab)
    {
        Vector3 BulletRota = ab.transform.eulerAngles;
        ab.transform.Rotate(new Vector3(0f, 0f, -angle));

        GameObject bullet = BP.GetPooledObject(NextBullet);//从对象池实例化子弹
        if (bullet != null)
        {
            bullet.transform.position = ab.transform.position;
            bullet.transform.eulerAngles = BulletRota;
            bullet.transform.Rotate(new Vector3(0f, 0f, angle));
            bullet.GetComponent<Bullet>().Damage = ab.GetComponent<Bullet>().Damage;
            bullet.SetActive(true);

        }
    }
    void SetMultiple3(GameObject ab)
    {
        Vector3 BulletRota = ab.transform.eulerAngles;

        GameObject bullet = BP.GetPooledObject(NextBullet);//从对象池实例化子弹
        if (bullet != null)
        {
            bullet.transform.position = ab.transform.position;
            bullet.transform.eulerAngles = BulletRota;
            bullet.transform.Rotate(new Vector3(0f, 0f, -angle));
            bullet.GetComponent<Bullet>().Damage = ab.GetComponent<Bullet>().Damage;
            bullet.SetActive(true);

        }
        GameObject bullet3 = BP.GetPooledObject(NextBullet);//从对象池实例化子弹
        if (bullet3 != null)
        {
            bullet3.transform.position = ab.transform.position;
            bullet3.transform.eulerAngles = BulletRota;
            bullet3.transform.Rotate(new Vector3(0f, 0f, angle));
            bullet3.GetComponent<Bullet>().Damage = ab.GetComponent<Bullet>().Damage;
            bullet3.SetActive(true);

        }
    }
    void SetMultiple4(GameObject ab)
    {
        Vector3 BulletRota = ab.transform.eulerAngles;
        ab.transform.Rotate(new Vector3(0f, 0f, angle));


        GameObject bullet = BP.GetPooledObject(NextBullet);
        if (bullet != null)
        {
            bullet.transform.position = ab.transform.position;
            bullet.transform.eulerAngles = BulletRota;
            bullet.transform.Rotate(new Vector3(0f, 0f, -angle));
            bullet.GetComponent<Bullet>().Damage = ab.GetComponent<Bullet>().Damage;
            bullet.SetActive(true);

        }

        GameObject bullet3 = BP.GetPooledObject(NextBullet);
        if (bullet3 != null)
        {
            bullet3.transform.position = ab.transform.position;
            bullet3.transform.eulerAngles = BulletRota;
            bullet3.transform.Rotate(new Vector3(0f, 0f, -angle * 3));
            bullet3.GetComponent<Bullet>().Damage = ab.GetComponent<Bullet>().Damage;
            bullet3.SetActive(true);

        }

        GameObject bullet4 = BP.GetPooledObject(NextBullet);
        if (bullet4 != null)
        {
            bullet4.transform.position = ab.transform.position;
            bullet4.transform.eulerAngles = BulletRota;
            bullet4.transform.Rotate(new Vector3(0f, 0f, angle * 3));
            bullet4.GetComponent<Bullet>().Damage = ab.GetComponent<Bullet>().Damage;
            bullet4.SetActive(true);
        }
    }

    public IEnumerator Shot()
    {
        if (ShotEnable == false)
        {
            yield return null;
        }
        else
        {
            if (HitImpact.activeInHierarchy == true)
                HitImpact.SetActive(false);
            HitImpact.SetActive(true);
            ShotEnable = false;
            print("发射第" + shoottime % shoot.Length + "槽位" + shoot[shoottime % shoot.Length]);
            shoottime++;
            GameObject bullet = BP.GetPooledObject(NextBullet);//从对象池实例化子弹
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = transform.position;         //子弹发射一刻的“位置”赋值
                bullet.transform.eulerAngles = transform.eulerAngles;   //子弹发射一刻的“方向”赋值

                //ExBullet.instance.SetDamage(bullet);
                SetMultipleShot(bullet);

            }
            yield return new WaitForSeconds(0.5f);
            ShotEnable = true;
        }
    }
}
