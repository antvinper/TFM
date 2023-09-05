using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private Slider soundSlider;
    [SerializeField] private AudioMixer masterMixer;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float val)
    {
        if(val < 1)
        {
            val = 0.001f;
        }

        RefreshSlider(val);
        PlayerPrefs.SetFloat("SavedMasterVolume", val);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(val / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundSlider.value);
    }

    public void RefreshSlider(float val)
    {
        soundSlider.value = val;
    }
}
