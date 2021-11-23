using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string headerText;
    [SerializeField] private string contentText;
    [SerializeField] private Sprite image;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(contentText, headerText, image);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}
