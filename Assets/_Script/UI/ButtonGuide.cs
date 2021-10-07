using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGuide : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string title;
    [SerializeField] string cotent;

    private void OnDisable()
    {
        ButtonManager.Instance.CloseGuide();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ButtonManager.Instance.GuideObj.transform.position = transform.position;
        ButtonManager.Instance.ShowGuide(title, cotent);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonManager.Instance.CloseGuide();
    }
}
