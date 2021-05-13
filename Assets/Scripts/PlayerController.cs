using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    /// <summary>
    /// 玩家刚体
    /// </summary>
    Rigidbody rd;

    /// <summary>
    /// 玩家移动速度
    /// </summary>
    public float speed = 10f;

    /// <summary>
    /// 当前速度
    /// </summary>
    float current_x = 0f;
    float current_y = 0f;

    /// <summary>
    /// 速度插值大小
    /// </summary>
    float v_lerp = 10f;

    /// <summary>
    /// 移动方向
    /// </summary>
    Vector3 direction;

    /// <summary>
    /// 当前移动方向
    /// </summary>
    Vector3 current_direction;

    /// <summary>
    /// 玩家动画控制器
    /// </summary>
    Animator animator;

    /// <summary>
    /// 跳跃控制
    /// </summary>
    bool wasGrounded = false;
    bool isGrounded = false;
    bool jumpInput = false;
    private float jumpTimeStamp = 0;
    private float minJumpInterval = 0.25f;

    /// <summary>
    /// 地面碰撞体
    /// </summary>
    private List<Collider> m_collisions = new List<Collider>();

    /// <summary>
    /// 相机Transform
    /// </summary>
    private Transform cameraTransform;



    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
                isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if (validSurfaceNormal)
        {
            isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        }
        else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { isGrounded = false; }
    }

    // Update is called once per frame
    void Update()
    {
        //输入检测
        if (!jumpInput && Input.GetKey(KeyCode.Space))
        {
            jumpInput = true;
        }

        //开火检测
        if(Input.GetButtonDown("Fire1"))
        {
            fire();
        }
        
    }

    // 物理引擎
    private void FixedUpdate()
    {

        animator.SetBool("Grounded", isGrounded);

        movement();

        wasGrounded = isGrounded;
        jump();
        jumpInput = false;
    }


    void movement()
    {
        float input_H = Input.GetAxis("Horizontal");
        float input_V = Input.GetAxis("Vertical");

        current_x = Mathf.Lerp(current_x, input_H, Time.deltaTime * v_lerp);
        current_y = Mathf.Lerp(current_y, input_V, Time.deltaTime * v_lerp);

        // 将要到达的方向
        direction = transform.right * current_x + transform.forward * current_y;

        // 向后走
        if(direction.z < 0)
        {
            animator.SetFloat("MoveSpeed", -direction.magnitude / 3 * 2);
        }
        // 其他方向

        else
        {
            animator.SetFloat("MoveSpeed", direction.magnitude);
        }
        
        if(direction != Vector3.zero)
        {
            // 方向的转变过程
            current_direction = Vector3.Lerp(current_direction, direction, Time.deltaTime * v_lerp);

            transform.position += current_direction * speed * Time.deltaTime;
        }

        

    }

    void jump()
    {
        // 两次jump冷却时间
        bool jumpCooldownOver = (Time.time - jumpTimeStamp) >= minJumpInterval;

        if (jumpCooldownOver && isGrounded && jumpInput)
        {
            jumpTimeStamp = Time.time;
            rd.AddForce(Vector3.up * 4, ForceMode.Impulse);
            //rd.velocity = new Vector3(rd.velocity.x, 10, rd.velocity.z);
        }

        if (!wasGrounded && isGrounded)
        {
            animator.SetTrigger("Land");
        }

        if (!isGrounded && wasGrounded)
        {
            animator.SetTrigger("Jump");
        }
    }

    void fire()
    {
        RaycastHit hit;

        if(Physics.Raycast(cameraTransform.position, cameraTransform.forward, 
            out hit, 15f))
        {
            Debug.Log(hit.collider.name);
            if(hit.collider.name != "Ground")
            {
                hit.collider.GetComponent<Health>().healthDamage(10);
            }
        }
    }
}
