using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperties
{
    [Range(1, 4)] public int Multiple;          //多重数量
    [Range(1, 4)] public int DamageLevel;       //伤害等级
    [Range(0, 2)] public int TrackBullet;       //追踪子弹数量（每次射击另外发出追踪子弹）
    [Range(1, 2.5f)] public float BulletRange;     //射程
    public string[] shoot = new string[3];        //子弹发射序列
    public WeaponProperties()
    {
        Multiple = 1;
        DamageLevel = 1;
        TrackBullet = 0;
        BulletRange = 1f;
        shoot[0] = "null";
        shoot[1] = "null";
        shoot[2] = "null";
    }

    public void RandomProperties()
    {
        Multiple = Random.Range(1, 5);
        DamageLevel = Random.Range(1, 5);
        TrackBullet = Random.Range(0, 3);
        BulletRange = Random.Range(1, 2.5f);
    }
}
