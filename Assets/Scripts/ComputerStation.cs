using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerStation : MonoBehaviour
{
    public Transform spawnPos;
    // ***存档点不再具有形态
    // public PlayerController.ShapeType shape;
    // [SerializeField] private SpriteRenderer shapeSprite;
    public void Start()
    {
        /*
        switch (shape)
        {
            case PlayerController.ShapeType.Basic:
                shapeSprite.sprite = Resources.Load<Sprite>("Sprite/ShapeBasic");
                break;
            case PlayerController.ShapeType.Fly:
                shapeSprite.sprite = Resources.Load<Sprite>("Sprite/ShapeFly");
                break;
            case PlayerController.ShapeType.Giant:
                shapeSprite.sprite = Resources.Load<Sprite>("Sprite/ShapeGiant");
                break;
            default:
                break;
        }
        */
    }

    public void Save()
    {
        GameManager.Instance.SwitchSavePoint(this);
    }
}
