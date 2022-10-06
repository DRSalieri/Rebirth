using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class VerticalPlatform : MonoBehaviour
{
    private enum MoveState
    { 
        ToTop,
        ToBottom
    };
    [SerializeField] private MoveState moveState;

    [SerializeField] private Transform topTransform;
    [SerializeField] private Transform bottomTransform;

    [SerializeField] private float speed;

    private float topY, bottomY;

    private void Start()
    {
        topY = topTransform.position.y;
        bottomY = bottomTransform.position.y;
        Destroy(topTransform.gameObject);
        Destroy(bottomTransform.gameObject);
    }

    private void Update()
    {
        float result = transform.position.y;

        if(moveState == MoveState.ToTop)
        {
            if(transform.position.y < topY)
            {
                result += Time.deltaTime * speed;
            }
        }
        else
        {
            if(transform.position.y > bottomY)
            {
                result -= Time.deltaTime * speed;
            }
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(result, bottomY, topY), transform.position.z);
    }

    public void MoveToTop()
    {
        moveState = MoveState.ToTop;
    }

    public void MoveToBottom()
    {
        moveState = MoveState.ToBottom;
    }

}
