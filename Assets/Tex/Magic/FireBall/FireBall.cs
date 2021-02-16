using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            GameObject impact = DUIXIANGCHI1.instance.GetImpactPooledObject("eew");//从对象池实例化子弹
            if (impact != null)
            {
                impact.transform.position = transform.position;
                impact.transform.rotation = transform.rotation;
                impact.SetActive(true);
            }
        }
    }
}
