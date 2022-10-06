using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 玩家控制器组件
// 挂在Player身上
public class PlayerController : MonoBehaviour
{
    // 形态
    public enum ShapeType
    {
        Basic,
        Fly,
        Giant
    };
    public ShapeType shape;
    // 技能
    public enum SkillType
    {
        platform
    };
    public SkillType skill;

    // 刚体、碰撞器
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Collider2D giantColl;
    [SerializeField] private Collider2D coll;
    [SerializeField] private Collider2D diedColl;
    [SerializeField] private Collider2D giantDiedColl;


    // Sprite
    [SerializeField] private Transform spriteTrans;

    // 跳跃相关
    public int JumpCount;       // 可跳跃次数
    private int _JumpCount;      // 剩余可跳跃次数
    [SerializeField] private float jumpForce;   // 跳跃力
    public bool canOperate;
    public bool isGround, jumpPressed, useSkill, isInteracting, isRun, isDied, isBoom, isBooming;
    public RaycastHit2D groundHit;

    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask npc;
    [SerializeField] private LayerMask ruin;
    [SerializeField] private Transform groundCheck;

    // 速度
    public float speed;

    // 技能
    [SerializeField] private Transform createPoint;

    // 其他参数
    [SerializeField] private float dialogDist;       // 在这个距离内按E才能触发对话

    // 动画控制
    [SerializeField] private Animator anim;

    // VFX
    [SerializeField] private GameObject BoomVFX1;
    [SerializeField] private Transform BoomVFX1trans;
    [SerializeField] private GameObject BoomVFX2;
    [SerializeField] private Transform BoomVFX2trans;

    [SerializeField] private PlayerAnim playerAnim;

    [SerializeField] private AudioSource diedAudio;
    [SerializeField] private AudioSource jumpAudio;

    private bool _isControlled = true;

    private void Start()
    {
        canOperate = true;
        // 初始化碰撞器
        diedColl.enabled = false;
        giantDiedColl.enabled = false;
        // 初始化跳跃
        _JumpCount = JumpCount;
        // 初始化形态
        SwitchShape(shape);
    }

    public void ChangeControl(bool controlled) {
        _isControlled = controlled;
        if (_isControlled) {

        } else {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)) {
            if (_isControlled) {
                ChangeControl(false);
            } else {
                ChangeControl(true);
            }
        }

        if (_isControlled) {


            if (canOperate == false)
                return;
            if (isDied)
                return;
            if (rb == null || coll == null)
                return;
            // 更新这一帧的各种状态

            /*
            groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, ground);
            
            if(groundHit.collider != null)
                isGround = true;
            else isGround = false;
            */

            //isJump = !isGround;
            useSkill = Input.GetButtonDown("Skill");
            isInteracting = Input.GetButtonDown("Interact");
            isBooming = Input.GetButtonDown("Boom");
            isRun = (Mathf.Abs(rb.velocity.x) > 0.1f);

            if (Input.GetButtonDown("Jump") && _JumpCount > 0)
            {
                jumpPressed = true;
            }

            // 操作
            //Jump();
            Skill();
            Boom();
            Interact();
            Crush();
            
            

            SwitchAnim();
        }
    }
    private void FixedUpdate()
    {
        if (_isControlled) {
            if (isDied)
                return;
            if (rb == null || coll == null)
                return;

            groundHit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, LayerMask.GetMask("Ruin", "Ground"));

            if (groundHit.collider != null)
                isGround = true;
            else isGround = false;

            Movement();
            Jump();
        }
        
    }

    private void SwitchAnim()
    {
        anim.SetBool("IsRun", isRun);
        anim.SetBool("IsJump", !isGround);
    }
    // 控制主角的左右移动
    private void Movement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        // 通过改变spriteTrans的localScale的x值，实现左右翻转
        if (horizontalMove != 0)
        {
            spriteTrans.localScale = new Vector3(horizontalMove * Mathf.Abs(spriteTrans.localScale.x), Mathf.Abs(spriteTrans.localScale.y), 1);
        }
    }

    // 控制主角的跳跃
    private void Jump()
    {
        // 如果踩在地面上，重置跳跃次数
        if (isGround)
        {
            _JumpCount = JumpCount;
        }

        // 起跳
        if (jumpPressed)
        {
            jumpAudio.Play();
            //isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            _JumpCount--;
            jumpPressed = false;
        }

    }

    // 控制主角的技能
    private void Skill()
    {
        if (isBoom)
            return;
        if (useSkill)
        {
            switch (skill)
            {
                case SkillType.platform:
                    Died(false);
                    break;
                default:
                    break;
            }
            useSkill = false;
        }
    }
    private void Boom()
    {
        if (isBoom == false && isBooming)
        {
            isBoom = true;

            anim.SetTrigger("IsBoom");
        }
    }
    private void Interact()
    {
        if (isInteracting)
        {
            Collider2D npcColl = Physics2D.OverlapCircle(transform.position, dialogDist, npc);
            if (npcColl)
            {
                if (npcColl.tag == "SavePoint")
                {
                    ComputerStation cs = npcColl.GetComponent<ComputerStation>();
                    cs.Save();
                    // SwitchShape(cs.shape);
                } else if(npcColl.tag == "Door")
                {
                    NextScene nextScene = npcColl.GetComponent<NextScene>();
                    nextScene.Use();
                } else if(npcColl.tag == "Info")
                {
                    InfoBoard IB = npcColl.GetComponent<InfoBoard>();
                    IB.ExecutePositiveBlock();
                } else if(npcColl.tag == "Switch")
                {
                    GroundSwitch GS = npcColl.GetComponent<GroundSwitch>();
                    GS.ClickButton();
                }
            }
        }
    }
    // 开始死亡动画
    private void Died(bool fromBoom)
    {
        if(fromBoom == false && isBoom && BoomVFX1trans != null)
        {
            Boom_End();
        }

        diedAudio.Play();
        isDied = true;
        // 刚体速度设为0，空中自由落体
        rb.velocity = new Vector2(0, 0);
        rb.constraints = ~RigidbodyConstraints2D.FreezePositionY;
        // 碰撞体设置
        coll.enabled = false;
        giantColl.enabled = false;
        if(shape == ShapeType.Giant)
        {
            diedColl.enabled = false;
            giantDiedColl.enabled = true;
        }
        else
        {
            diedColl.enabled = true;
            giantDiedColl.enabled = false;
        }
        
        // tag设置
        this.tag = "Ruin";
        // Layermask
        this.gameObject.layer = LayerMask.NameToLayer("Ruin");

        GameManager.Instance.OnRuinCreate(this.gameObject);


        anim.SetTrigger("IsDied");
    }
    // 死亡动画结束，重生
    public void Rebirth()
    {
        // 挂载ruin脚本
        this.gameObject.AddComponent<Ruin>();
        Destroy(this);
        GameManager.Instance.Rebirth();
    }

    // Boom动画刚开始
    public void Boom_Start()
    {
        BoomVFX1trans = Instantiate(BoomVFX1, this.transform).transform;
    }

    // Boom动画结束
    public void Boom_End()
    {
        Destroy(BoomVFX1trans.gameObject);
        BoomVFX2trans = Instantiate(BoomVFX2, this.transform).transform;
        isBoom = false;
        Died(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!isDied)
        {
            if (other.tag == "DeadZone")
            {
                Destroy(this.gameObject);
                GameManager.Instance.Rebirth();
            }
            else if(other.tag == "Spike")
            {
                Died(false);
            }
            else if(other.tag == "Lazer")
            {
                Died(false);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(!isDied)
        {
            if(other.gameObject.tag == "Enemy")
            {
                if(other.gameObject.GetComponent<EnemyMove>().isDied == false)
                    Died(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(transform.position, dialogDist);
        */
    }
    private void Crush()
    {
        if (isGround)
        {
            if (groundHit.collider != null && groundHit.collider.tag == "Fragile" && shape == ShapeType.Giant)
            {
                // 破坏地面
                groundHit.collider.GetComponent<Fragile>().Crush();
            }
        }
    }

    public void SwitchShape(ShapeType _s)
    {
        shape = _s;
        switch (_s)
        {
            case ShapeType.Basic:
                JumpCount = 1;
                giantColl.enabled = false;
                coll.enabled = true;
                playerAnim.TurnGiant(false);

                break;
            case ShapeType.Fly:
                JumpCount = 2;
                giantColl.enabled = false;
                coll.enabled = true;
                 playerAnim.TurnGiant(false);

                break;
            case ShapeType.Giant:
                JumpCount = 1;
                giantColl.enabled = true;
                coll.enabled = false;
                 playerAnim.TurnGiant(true);

                break;
            default:
                break;
        }

        // 更新UI
        GameManager.Instance.UI_StatusImg.Refresh(_s);
    }

}
