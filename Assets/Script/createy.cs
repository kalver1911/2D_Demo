using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class createy : MonoBehaviour
{
    [Header("这里不用动了")]
    ExBullet EX;
    public bool Reset=false;
    public StringBuilder Skill = new StringBuilder();
    public Dictionary<string, Action> UseSkill = new Dictionary<string, Action>();  //放里面的函数要无参无返回值


    [Header("把技能所需要的Gameobject放这里")]        //不是Gameobject也行
    //当前还需写技能生成的位置
    public GameObject qqq;
    public GameObject eew;
    public GameObject eeq;
    void Awake()
    {
        EX = ExBullet.instance;
        Combine();
        UseSkill.Add("eeq", EEQ);
        UseSkill.Add("qqq", QQQ);
        UseSkill.Add("eew", EEW);       //把写好的技能加到字典里面
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (UseSkill.ContainsKey((Skill.ToString())))
            {
                UseSkill[Skill.ToString()].Invoke();
            }
            else {
                print("无");
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
                Combine();
        }
    }
    void Combine()
    {
        Skill.Remove(0, Skill.Length);
        Skill.Append(EX.shoot[0]).Append(EX.shoot[1]).Append(EX.shoot[2]);
    }

    //把技能全部写在这下面
    void EEW()
    {
        Instantiate(eew, transform.position, transform.rotation);
    }
    void QQQ()
    {
        Instantiate(qqq, transform.position, transform.rotation);
    }
    void EEQ()      //火盾，火盾先前就挂在角色身上，直接Active就行
    {
        eeq.SetActive(true);
    }
}
