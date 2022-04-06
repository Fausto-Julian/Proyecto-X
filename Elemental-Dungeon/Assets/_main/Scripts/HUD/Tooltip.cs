using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerField;
    [SerializeField] private TextMeshProUGUI contentField;
    [SerializeField] private LayoutElement _layoutElement;
    [SerializeField] private Image image;

    private RectTransform _rectTransform;
    private int _characterLimit = 40;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLength = headerField.text.Length;
            int contentLength = contentField.text.Length;

            _layoutElement.enabled = (headerLength > _characterLimit || contentLength > _characterLimit) ? true : false;
        }

        Vector2 mousePosition = Input.mousePosition;

        //float pivotX = mousePosition.x / Screen.width;
        //float pivotY = mousePosition.y / Screen.height;

        //_rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = mousePosition;
    }

    public void SetImage(Sprite imageSprite)
    {
        if (imageSprite != null)
            image.sprite = imageSprite;

    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }
        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        _layoutElement.enabled = (headerLength > _characterLimit || contentLength > _characterLimit) ? true : false;
    }
}
