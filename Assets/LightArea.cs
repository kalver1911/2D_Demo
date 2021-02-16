using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightArea : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask LM;

    [Range(0f, 100f)] public float force;
    public Vector3 a;

    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //a = MouseFollow()-transform.position;
        transform.position = MouseFollow();
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //        collision.gameObject.transform.right= Vector3.Slerp(collision.gameObject.transform.right, a.normalized, force / 100);
    //    }
    //}

    public Vector3 MouseFollow()
    {
        //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
        screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;
        //让场景中的Z=鼠标坐标的Z
        mousePositionOnScreen.z = screenPosition.z;
        //将相机中的坐标转化为世界坐标
        mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动
        return new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, mousePositionInWorld.z);
    }
}
