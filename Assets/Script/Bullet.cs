using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("固定的东西")]
    public Vector3 a;
    public string myName;
    private Rigidbody2D rb;


    [Header("后期会改的东西")]
    public float Damage = 2;      
    public float Speed = 17f;
    public float Range = 1.5f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        a = transform.eulerAngles;
    }
    void OnEnable()
    {
        StartCoroutine(ThreeSecond());
    }
    void FixedUpdate()
    {
        rb.transform.position += transform.right * Time.deltaTime * Speed;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //gameObject.transform.eulerAngles = a;       //这里到时候要弄一个专门初始化子弹数据的函数
            gameObject.SetActive(false);
            if (collision.gameObject.GetComponent<Health>() != null)
            {
                collision.gameObject.GetComponent<Health>().HP -= Damage;
            }
            //GameObject impact = BulletPool.instance.GetImpactPooledObject(myName);//从对象池实例化子弹
            //                                                                       //得到当前子弹是什么类型然后实例化对应的爆炸火花（到时候可能需要重构）

            //if (impact != null)
            //{
            //    impact.transform.position = transform.position;
            //    impact.transform.rotation = transform.rotation;
            //    impact.SetActive(true);
            //}


            if (collision.gameObject.GetComponent<FSM>() != null)
            {
                collision.gameObject.GetComponent<FSM>().BlockTest();
            }
            Impact();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            Impact();
        }
    }
    IEnumerator ThreeSecond()
    {
        yield return new WaitForSeconds(Range);
        gameObject.SetActive(false);
        Impact();
    }
    void Impact()
    {
        gameObject.SetActive(false);
        GameObject impact = BulletPool.instance.GetImpactPooledObject(myName);//从对象池实例化子弹
        if (impact != null)
        {
            impact.transform.position = transform.position;
            impact.transform.rotation = transform.rotation;
            impact.SetActive(true);
        }
        gameObject.transform.eulerAngles = a;       //这里到时候要弄一个专门初始化子弹数据的函数
    }

}
