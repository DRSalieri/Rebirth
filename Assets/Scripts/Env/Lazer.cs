using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    [SerializeField] private Transform lazerLeftTransform;
    [SerializeField] private Transform lazerRightTransform;
    [SerializeField] private Transform lazerTopTransform;
    [SerializeField] private Transform lazerBottomTransform;

    public void TurnOnLeftLazer()
    {
        lazerLeftTransform.gameObject.SetActive(true);
    }
    public void TurnOnRightLazer()
    {
        lazerRightTransform.gameObject.SetActive(true);
    }
    public void TurnOnTopLazer()
    {
        lazerTopTransform.gameObject.SetActive(true);
    }
    public void TurnOnBottomLazer()
    {
        lazerBottomTransform.gameObject.SetActive(true);
    }
    public void TurnOffLeftLazer()
    {
        lazerLeftTransform.gameObject.SetActive(false);
    }
    public void TurnOffRightLazer()
    {
        lazerRightTransform.gameObject.SetActive(false);
    }
    public void TurnOffTopLazer()
    {
        lazerTopTransform.gameObject.SetActive(false);
    }
    public void TurnOffBottomLazer()
    {
        lazerBottomTransform.gameObject.SetActive(false);
    }
}
