using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundButton : MonoBehaviour
{
    [SerializeField] private Collider2D colliderDown;
    [SerializeField] private Collider2D colliderUp;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Sprite buttonUp;
    [SerializeField] private Sprite buttonDown;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private UnityEvent upAction;
    [SerializeField] private UnityEvent downAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Down();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Up();
        }
    }

    private void Down()
    {
        sr.sprite = buttonDown;
        downAction.Invoke();
    }

    private void Up()
    {
        sr.sprite = buttonUp;
        upAction.Invoke();
    }

}
