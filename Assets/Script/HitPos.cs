using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPos : MonoBehaviour
{
    static public HitPos instance; 

    public LayerMask LM;    //Layer层（未使用）
    public Vector3 Moupos;                          //鼠标坐标
    public Vector3 MouposToGround;                  //鼠标坐标到地面的坐标
    [Range(0f, 20f)] public float RayRange = 20;    //射线长度

    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePositionInWorld;//将点击屏幕的屏幕坐标转换为世界坐标
    void Awake()
    {
        instance = this;
        Physics2D.queriesStartInColliders = false;
    }

    void Update()
    {
        transform.position = MouseFollow();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, RayRange);
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Ground"))
            {
                MouposToGround = hit.point;
                //print(MouposToGround);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, -transform.up * RayRange);
    }
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
    public Vector3 ReturnMouseTOGroudPos()
    {
        return MouposToGround;
    }
}
