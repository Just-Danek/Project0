using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SettingsMenuEnv : MonoBehaviour
{
    public Slider volumeSlider;
    public List<AudioSource> sfxAudioSources;
    public AudioMixer audioMixer;

    void Start()
    {
        // ������������� �������� �������� �� StaticHolder
        volumeSlider.value = StaticHolder.EnvVolume;
        SetVolume(StaticHolder.EnvVolume);
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
    }

    void OnVolumeChanged(float volume)
    {
        SetVolume(volume);
        StaticHolder.EnvVolume = volume; // ��������� ��������
    }

    public void SetVolume(float volume)
    {
        // �������� ��������� � ������� ������ �� -80 �� 0 ��
        // ����� ��������� �� ���� ���������������, ��������� �������� �����
        float dB = Mathf.Lerp(-30f, 10f, volume); // volume �� 0 �� 1
        audioMixer.SetFloat("EnvVolume", dB);

        // ��������� � StaticHolder, ���� �����
        StaticHolder.EnvVolume = volume;
    }
}