using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusImg : MonoBehaviour
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }
    public void Refresh(PlayerController.ShapeType _s)
    {
        switch (_s)
        {
            case PlayerController.ShapeType.Basic:
                image.sprite = Resources.Load<Sprite>("Sprite/ShapeBasic");
                break;
            case PlayerController.ShapeType.Fly:
                image.sprite = Resources.Load<Sprite>("Sprite/ShapeFly");
                break;
            case PlayerController.ShapeType.Giant:
                image.sprite = Resources.Load<Sprite>("Sprite/ShapeGiant");
                break;
            default:
                break;
        }
    }
}
