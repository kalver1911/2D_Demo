using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家基本属性的脚本
/// </summary>
public class PlayerControl : MonoBehaviour
{
    float totalProgress;              //总血量 
    float currentProgress;            //当前血量

    public void GetInjured(float damage)            //受到伤害
    {
        if (currentProgress <= damage)
        {
            currentProgress = 0.0f;

            //玩家已死亡
            //do something
        }
        else
        { currentProgress -= damage; }
    }

    public void Treated(float healing)              //治疗
    {
        if (totalProgress - currentProgress <= healing)   //空余血量少于治疗值
        {
            currentProgress = totalProgress;
        }
        else
        {
            currentProgress += healing;
        }
    }
}
