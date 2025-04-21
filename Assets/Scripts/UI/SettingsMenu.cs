using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer; // �������� MainAudioMixer
    public Slider sfxSlider; // �������� ������� �� Canvas

    void Start()
    {
        // ��������� ���������� ��������� ��� ������
        if (PlayerPrefs.HasKey("GunVolume"))
        {
            float volume = PlayerPrefs.GetFloat("GunVolume");
            sfxSlider.value = volume;
            SetVolume(volume);
        }

        sfxSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // �� 0 �� 1 � �������� ��������
        float db = (volume == 0f) ? -80f : Mathf.Lerp(-40f, 0f, volume); // -40dB = ����� ��������, 0dB = ����
        audioMixer.SetFloat("GunVolume", db);
    }
}
