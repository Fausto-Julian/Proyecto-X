using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    [SerializeField] private Tooltip tooltip;

    private void Awake()
    {
        current = this;
    }

    public static void Show(string content, string header = "", Sprite imageSprite = null)
    {
        current.tooltip.SetImage(imageSprite);
        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        try
        {
            current.tooltip.gameObject.SetActive(false);
        }
        catch
        {
            Debug.LogError("Herror en hide tooltip");
        }
    }
}
