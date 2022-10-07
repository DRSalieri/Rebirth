using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelescopicSpikes : MonoBehaviour
{
    private enum SpikeState
    {
        ToTop,
        ToBottom
    }
    [SerializeField] private SpikeState spikeState;

    private float topY, bottomY;
    [SerializeField] private Transform topTransform;
    [SerializeField] private Transform bottomTransform;

    [SerializeField] private Transform spritesTransform;

    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        topY = topTransform.position.y;
        bottomY = bottomTransform.position.y;
        Destroy(topTransform.gameObject);
        Destroy(bottomTransform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        float result = spritesTransform.position.y;
        if(spikeState == SpikeState.ToTop)
        {
            if(spritesTransform.position.y < topY)
            {
                result += speed * Time.deltaTime;
            }
        }
        else if(spikeState == SpikeState.ToBottom)
        {
            if(spritesTransform.position.y > bottomY)
            {
                result -= speed * Time.deltaTime;
            }
        }
        spritesTransform.position = new Vector3(spritesTransform.position.x, Mathf.Clamp(result, bottomY, topY), spritesTransform.position.z);
    }

    public void MoveToTop()
    {
        spikeState = SpikeState.ToTop;
    }

    public void MoveToBottom()
    {
        spikeState = SpikeState.ToBottom;
    }
}
