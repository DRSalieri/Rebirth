using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopicPlatform : MonoBehaviour
{
    private enum PlatformState
    {
        ToLeft,
        ToRight,
        ToUp,
        ToDown
    };
    [SerializeField] private PlatformState platformSate;

    private float leftX, rightX;
    [SerializeField] private Transform leftTransform;
    [SerializeField] private Transform rightTransform;

    [SerializeField] private Transform spritesTransform;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        leftX = leftTransform.position.x;
        rightX = rightTransform.position.x;
        Destroy(leftTransform.gameObject);
        Destroy(rightTransform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float result  = spritesTransform.position.x;
        if(platformSate == PlatformState.ToLeft)
        {
            if(spritesTransform.position.x > leftX)
            {
                result -= speed * Time.deltaTime;
            }
            spritesTransform.position = new Vector3(Mathf.Clamp(result, leftX, rightX), spritesTransform.position.y, spritesTransform.position.z);
        }
        else if (platformSate == PlatformState.ToRight)
        {
            if(spritesTransform.position.x < rightX)
            {
                result += speed * Time.deltaTime;
            }
            spritesTransform.position = new Vector3(Mathf.Clamp(result, leftX, rightX), spritesTransform.position.y, spritesTransform.position.z);
        } else {
            result = spritesTransform.position.y;
            if (platformSate == PlatformState.ToUp) {
                if(spritesTransform.position.y < leftX)
                {
                    result += speed * Time.deltaTime;
                }
            } else {
                if(spritesTransform.position.y > rightX)
                {
                    result -= speed * Time.deltaTime;
                }
            }
            spritesTransform.position = new Vector3(spritesTransform.position.x, Mathf.Clamp(result, leftX, rightX), spritesTransform.position.z);
        }
    }

    public void MoveToLeft()
    {
        platformSate = PlatformState.ToLeft;
    }

    public void MoveToRight()
    {
        platformSate = PlatformState.ToRight;
    }
    public void MoveToUp()
    {
        platformSate = PlatformState.ToUp;
    }

    public void MoveToDown()
    {
        platformSate = PlatformState.ToDown;
    }
}
