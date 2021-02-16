using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Pool
{
    public List<GameObject> PooledObjects = new List<GameObject>();
    public int PooledAmount = 20;                        //子弹池初始大小
    public int CurrentIndex = 0;                       //当前指向链表位置索引
}
