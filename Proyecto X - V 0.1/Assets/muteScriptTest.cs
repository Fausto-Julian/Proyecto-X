using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class muteScriptTest : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup music;
    [SerializeField] private Sprite audioOn, audioOff;
    private Button muteButton;
    private Image img;
    private bool mute = false;

    private void Awake()
    {
        muteButton = GetComponent<Button>();
        img = GetComponent<Image>();
    }

    private void Start()
    {
        img.sprite = audioOn;
        muteButton.onClick.AddListener(MuteControlllerHandler);
    }

    private void MuteControlllerHandler()
    {
        if (mute)
        {
            img.sprite = audioOn;
            mute = false;
            
            music.audioMixer.SetFloat("MusicVolumen", 10);
        }
        else
        {
            img.sprite = audioOff;
            mute = true;
            music.audioMixer.SetFloat("MusicVolumen", -80);
        }
    }
}
