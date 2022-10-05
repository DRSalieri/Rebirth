using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GroundSwitch : MonoBehaviour
{
    private enum SwitchState {
        Up,
        Down
    };
    private SwitchState state;

    [SerializeField] private Collider2D collider;

    [SerializeField] private Sprite switchUp;
    [SerializeField] private Sprite switchDown;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private UnityEvent clickAction;



    private void Start()
    {
        state = SwitchState.Up;
        sr.sprite = switchUp;
    }

    public void ClickButton()
    {
        if (state == SwitchState.Up)
        {
            state = SwitchState.Down;
            sr.sprite = switchDown;
            clickAction.Invoke();
        }
    }
}
