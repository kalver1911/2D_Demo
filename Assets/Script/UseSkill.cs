using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UseSkill : MonoBehaviour
{
    public GameObject In;

    [Header("这里不用动了")]
    public ExBullet EX;
    public GameObject GunPoint;
    public StringBuilder Skill = new StringBuilder();
    public Dictionary<string, Action> SkillData = new Dictionary<string, Action>();  //放里面的函数要无参无返回值

    public HitPos hitPos;


    //当前还需写技能生成的位置
    //脱手的技能gameobject放在了对象池，这里放的是特殊的gameobject
    [Header("把技能所需要的Gameobject放这里")]
    [Header("这里的技能是早先挂在在人物或场景的技能")]
    public GameObject qqq;  //暴风雪
    public GameObject qqw;
    public GameObject wwq;
    public GameObject eeq;  //火盾


    public GameObject aba;
    public GameObject bab;
    void Start()
    {
        EX = ExBullet.instance;
        hitPos = HitPos.instance;
        Combine();
        SkillData.Add("qqq", QQQ);
        SkillData.Add("qqw", QQW);
        SkillData.Add("qqe", QQE);
        SkillData.Add("wwe", WWE);
        SkillData.Add("wwq", WWQ);
        SkillData.Add("eeq", EEQ);
        SkillData.Add("eee", EEE);
        SkillData.Add("eew", EEW);       //把写好的技能加到字典里面
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (SkillData.ContainsKey((Skill.ToString())))
            {
                StartCoroutine(InvokSkill());
            }
            else {
                print("无");
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
                Combine();
        }
    }
    void Combine()
    {
        Skill.Remove(0, Skill.Length);
        Skill.Append(EX.shoot[0]).Append(EX.shoot[1]).Append(EX.shoot[2]);
    }

    IEnumerator InvokSkill()
    {
        In.SetActive(true);
        Time.timeScale = 0.3f;
        yield return new WaitUntil(()=>In.activeInHierarchy==false);
        Time.timeScale = 1f;
        SkillData[Skill.ToString()].Invoke();
        yield return null;
    }

    //把技能全部写在这下面
    void EEW()
    {
        GameObject bullet = DUIXIANGCHI1.instance.GetPooledObject("eew");//从对象池实例化子弹
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = aba.transform.position;         //子弹发射一刻的“位置”赋值
            bullet.transform.eulerAngles = transform.eulerAngles;   //子弹发射一刻的“方向”赋值
            bullet.transform.up = bullet.transform.position - hitPos.ReturnMouseTOGroudPos();
        }
    }
    void QQQ()
    {
        qqq.SetActive(true);
    }
    void EEQ()      //火盾，火盾先前就挂在角色身上，直接Active就行
    {
        eeq.SetActive(true);
    }
    void EEE()
    {
        GameObject bullet = DUIXIANGCHI1.instance.GetPooledObject("eee");//从对象池实例化子弹
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = GunPoint.transform.position;         //子弹发射一刻的“位置”赋值
            bullet.transform.eulerAngles = GunPoint.transform.eulerAngles;   //子弹发射一刻的“方向”赋值
        }
    }
    void WWE()
    {
        //GameObject bullet = DUIXIANGCHI1.instance.GetPooledObject("wwe");//从对象池实例化子弹
        //if (bullet != null)
        //{
        //    //bullet.SetActive(true);
        //    //bullet.transform.position = transform.position;         //子弹发射一刻的“位置”赋值
        //    //bullet.transform.eulerAngles = transform.eulerAngles;   //子弹发射一刻的“方向”赋值


        //    bullet.SetActive(true);
        //    bullet.transform.position = new Vector2(hitPos.ReturnMouseTOGroudPos().x, aba.transform.position.y);         //子弹发射一刻的“位置”赋值
        //    bullet.transform.eulerAngles = transform.eulerAngles;   //子弹发射一刻的“方向”赋值
        //    //bullet.transform.up = bullet.transform.position - hitPos.ReturnMouseTOGroudPos();
        //    bullet.transform.Rotate(new Vector3(0, 0, -180));       //预制体那里方向是竖起来的，所以发射前要旋转成横向
        //}
        StartCoroutine(ieWWE());
    }
    IEnumerator ieWWE()
    {
        Vector2[] a = new Vector2[7];
        a[1]=new Vector2(hitPos.ReturnMouseTOGroudPos().x, aba.transform.position.y);
        for (int i = 2; i <7; i++)
        {
            a[i] = new Vector2(hitPos.ReturnMouseTOGroudPos().x + UnityEngine.Random.Range(-3,3), aba.transform.position.y);
        }
        for (int i = 1; i <7; i++)
        {
            GameObject bullet = DUIXIANGCHI1.instance.GetPooledObject("wwe");//从对象池实例化子弹
            if (bullet != null)
            {
                bullet.SetActive(true);
                bullet.transform.position = a[i];
                bullet.transform.eulerAngles = transform.eulerAngles;   //子弹发射一刻的“方向”赋值
                bullet.transform.Rotate(new Vector3(0, 0, -180));       //预制体那里方向是竖起来的，所以发射前要旋转成横向
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return null;
    }
    void QQE()
    {
        GameObject bullet = DUIXIANGCHI1.instance.GetPooledObject("qqe");//从对象池实例化子弹
        if (bullet != null)
        {
            bullet.SetActive(true);
            bullet.transform.position = GunPoint.transform.position;         //子弹发射一刻的“位置”赋值
            bullet.transform.eulerAngles = GunPoint.transform.eulerAngles;   //子弹发射一刻的“方向”赋值
            bullet.transform.Rotate(new Vector3(0, 0, -90));
        }
    }
    void QQW()
    {
        qqw.SetActive(true);
    }
    void WWQ()
    {
        wwq.SetActive(true);
    }
}
