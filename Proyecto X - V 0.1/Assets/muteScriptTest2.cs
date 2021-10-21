using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class muteScriptTest2 : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup music;
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = 0;
        slider.minValue = -50;
    }

    private void Update()
    {
        music.audioMixer.SetFloat("MusicVolumen", slider.value);
    }
}
