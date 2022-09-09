using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public Collider2D normalColl;
    public Collider2D diedColl;
    public Transform leftPoint, rightPoint, spriteTrans;
    public Animator anim;
    private bool faceLeft = true;
    private float leftX;
    private float rightX;
    public float speed;
    public bool isDied, isGround;
    public float jumpForce;
    public Transform groundCheck;
    public float beforeX;
    public float remainTime;

    public AudioSource diedAudio;
    void Start()
    {
        remainTime = 0.5f;
        beforeX = transform.position.x;
        // 碰撞体设置
        normalColl.enabled = true;
        diedColl.enabled = false;

        isDied = false;
        leftX = leftPoint.position.x;
        rightX = rightPoint.position.x;
        Destroy(leftPoint.gameObject);
        Destroy(rightPoint.gameObject);
    }

    void Update()
    {
        if (!isDied)
        {
            isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, LayerMask.GetMask("Ruin", "Ground"));

            remainTime -= Time.deltaTime;

            if(remainTime <= 0f)
            {
                // 每0.5s检查一次，是否停留在了原地
                if(isGround && Mathf.Abs(transform.position.x - beforeX) < 0.000001f)
                    Jump();
                
                beforeX = transform.position.x;
                remainTime = 0.5f;
            }

            Movement();
            if (faceLeft)
                spriteTrans.localScale = new Vector3(-1, 1, 1);
            else
                spriteTrans.localScale = new Vector3(1, 1, 1);

            SwitchAnim();
        }
    }
    private void SwitchAnim()
    {
        anim.SetBool("IsGround", isGround);
    }
    public void Died()
    {
        diedAudio.Play();
        isDied = true;
        rb.velocity = new Vector2(0, rb.velocity.y);
        normalColl.enabled = false;
        diedColl.enabled = true;
        anim.SetTrigger("IsDied");
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Movement()
    {
        if (faceLeft)
        {
            rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
            //transform.position += new Vector3(-1 * speed * Time.deltaTime, 0, 0);
            if (transform.position.x < leftX)
            {
                faceLeft = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            // transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (transform.position.x > rightX)
            {
                faceLeft = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isDied)
        {
            if (other.gameObject.tag == "Ruin")
            {
                if (other.contacts[0].normal.y < 0)
                {
                    //if (Mathf.Abs(other.gameObject.GetComponent<Rigidbody2D>().velocity.y) > 1)
                        Died();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isDied)
        {
            if (other.tag == "Spike")
                Died();
        }
    }
}
