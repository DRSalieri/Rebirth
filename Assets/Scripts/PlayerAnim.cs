using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 里面保存animator需要调用的事件
public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private PlayerController pc;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Sprite diedSprite;
    [SerializeField] private Animator anim;
    public void Rebirth()
    {
        sr.sprite = diedSprite;
        Destroy(anim);
        pc.Rebirth();
    }

    public void TurnGiant(bool isGiant)
    {
        if(isGiant)
        {
            transform.localScale = new Vector3(1.5f,1.5f,1);
            transform.localPosition = new Vector3(0,0.25f,0);
        }
        else
        {
            transform.localScale = new Vector3(1,1,1);
            transform.localPosition = new Vector3(0,0,0);
        }
    }
    // Boom动画刚开始
    public void Boom_Start()
    {
        pc.Boom_Start();
    }

    // Boom动画结束
    public void Boom_End()
    {
        pc.Boom_End();
    }
}
