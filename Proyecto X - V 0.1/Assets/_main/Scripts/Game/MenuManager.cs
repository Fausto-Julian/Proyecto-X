using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Button openPanel;
    [SerializeField] private Button closePanel;

    private void Awake()
    {
        openPanel.onClick.AddListener(OnClickQuitPanelActivate);
        closePanel.onClick.AddListener(OnClickQuitPanelDesactivate);
    }

    public void OnClickQuitPanelActivate()
    {
        panel.SetActive(true);
    }

    public void OnClickQuitPanelDesactivate()
    {
        panel.SetActive(false);
    }
}
