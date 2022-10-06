using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DropDoor : MonoBehaviour
{
    private enum DropDoorState
    {
        ToTop,
        ToBottom
    };
    [SerializeField] private DropDoorState dropDoorState;

    // downTransform的localPosition
    private float topLocalY = 2.2f;
    private float bottomLocalY = 0f;

    [SerializeField] private Transform downTransform;

    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        float result = downTransform.localPosition.y;

        if (dropDoorState == DropDoorState.ToTop)
        {
            if (downTransform.localPosition.y < topLocalY)
            {
                result += Time.deltaTime * speed;
            }
        }
        else
        {
            if (downTransform.localPosition.y > bottomLocalY)
            {
                result -= Time.deltaTime * speed;
            }
        }

        downTransform.localPosition = new Vector3(downTransform.localPosition.x, Mathf.Clamp(result, bottomLocalY, topLocalY), downTransform.localPosition.z);
    }

    public void MoveToTop()
    {
        dropDoorState = DropDoorState.ToTop;
    }

    public void MoveToBottom()
    {
        dropDoorState = DropDoorState.ToBottom;
    }
}
