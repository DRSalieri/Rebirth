using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragile : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D coll;
    [SerializeField] private Rigidbody2D rb;
    public void Crush()
    {
        anim.SetTrigger("IsCrush");
    }
    public void DestroyRb()
    {
        Destroy(rb);
        Destroy(coll);
    }
    public void DestroySelf()
    {
        Destroy(this.gameObject);
    }
}
