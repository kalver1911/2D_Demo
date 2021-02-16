using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("这里基本不用动了")]
    private Rigidbody2D rb;
    private Animator anim;
    public float xVelocity;     //AD键入信息
    public float speed = 8f;
    public bool OnGround;
    public Transform checkPoint;
    public LayerMask layerMask;
    public Vector2 checkBoxSize;


    [Header("Better Jump")]
    //[SerializeField] private float jumpFactor;
    [SerializeField] private float fallFactor;
    [SerializeField] private float shortJumpFactor;

    [Header("判断粒子系统是否启用")]
    public GameObject SnowWalk;         //若这个处于激活状态，移动速度修正为12
    public GameObject LightningShoot;


    public HitPos hitPos;
    void Start()
    {
        hitPos = HitPos.instance;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        xVelocity = Input.GetAxis("Horizontal");
        if ((Input.GetKeyDown(KeyCode.Space))&& OnGround)
        {
            Jump();
        }
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(ExBullet.instance.Shot());
            anim.SetFloat("Shoot", 1);
        }
        else 
        {
            anim.SetFloat("Shoot", 0);
        }
    }
    void FixedUpdate()
    {
        anim.SetFloat("VerticalVelocity", rb.velocity.y);
        anim.SetBool("OnGround", OnGround);
        anim.SetFloat("Forward", Mathf.Abs(xVelocity));
        FlipDirection2();
        GroundMove();
        CheckGround();
        BetterJump();
    }
    void FlipDirection()
    {
        if (xVelocity > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            return;
        }
        else if (xVelocity < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    void FlipDirection2()
    {
        if ((hitPos.ReturnMouseTOGroudPos().x-transform.position.x)>0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            return;
        }
        else if ((hitPos.ReturnMouseTOGroudPos().x - transform.position.x) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    void GroundMove()       //移动
    {
        rb.velocity = new Vector2(xVelocity * (SnowWalk.activeInHierarchy==false?8:12), rb.velocity.y);
    }
    private void Jump()     //按下跳跃
    {
        rb.velocity = Vector2.up * 8;
        anim.SetTrigger("Jump");
    }
    private void CheckGround()
    {
        Collider2D collider = Physics2D.OverlapBox(checkPoint.position, checkBoxSize,0, layerMask); //绘制长方形，地面检测
        if (collider != null)
        {
            OnGround = true;
        }
        else 
        {
            OnGround = false;
        }
    }
    private void OnDrawGizmos() //可视化地面检测长方形

    {
        Gizmos.DrawWireCube(checkPoint.position, checkBoxSize);
        Gizmos.color = Color.red;
    }
    private void BetterJump()
    {
        if (rb.velocity.y < 0)//MARKER 角色下落时，速度会越来越快
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * fallFactor * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.K))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * shortJumpFactor * Time.deltaTime;
        }
    }
}
