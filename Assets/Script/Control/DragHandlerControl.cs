using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandlerControl : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    private Vector3 offset;

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        print("IBeginDragHandler.OnBeginDrag");

        print("这是实现的拖拽开始接口");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        print("IDragHandler.OnDrag");
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset.z = 90;
        Debug.Log("offset:" + offset);
        transform.position=offset;
        print("拖拽中……");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        print("IEndDragHandler.OnEndDrag");
        
        print("实现的拖拽结束接口");
    }
}
