using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSword : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    int speedC;
    public GameObject Shape;
    public GameObject Pos;
    SpriteRenderer SR;
    void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        speedC = speed;
    }
    void OnEnable()
    {
        StartCoroutine(InvokSkill());
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")|| collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            GameObject impact = DUIXIANGCHI1.instance.GetImpactPooledObject("qqe");//从对象池实例化子弹
            if (impact != null)
            {
                impact.transform.position = Pos.transform.position;
                impact.transform.rotation = Pos.transform.rotation;
                impact.SetActive(true);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Freeze.SetActive(true);
            gameObject.SetActive(false);
            GameObject impact = DUIXIANGCHI1.instance.GetImpactPooledObject("qqe");//从对象池实例化子弹
            if (impact != null)
            {
                impact.transform.position = Pos.transform.position;
                impact.transform.rotation = Pos.transform.rotation;
                impact.SetActive(true);
            }
        }
    }
    IEnumerator InvokSkill()
    {
        speed = 0;
        SR.enabled = false;
        Shape.SetActive(true);
        yield return new WaitUntil(() => Shape.activeInHierarchy == false);
        SR.enabled = true;
        speed = speedC;
    }
}
