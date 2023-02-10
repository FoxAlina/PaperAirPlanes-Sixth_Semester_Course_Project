using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public AudioSource audio;
    public Slider slider;
    public Toggle toggle;

    [SerializeField]
    private string saveKey;

    private float volume;
    private int isStopped;

    void Awake()
    {
        Load();

        if (isStopped == 2)
        {
            audio.Play();
            toggle.isOn = true;
        }
        else if (isStopped == 1)
        {
            audio.Stop();
            toggle.isOn = false;
        }

        audio.volume = volume;
        slider.value = volume;

        Save();
    }

    void Update()
    {
        if (toggle.isOn)
        {
            if (isStopped != 2)
            {
                isStopped = 2;
                audio.Play();
            }

            volume = slider.value;
            if (audio.volume != volume)
            {
                PlayerPrefs.SetFloat(saveKey, volume);
                audio.volume = volume;
            }

            Save();
        }
        else
        {
            if (isStopped != 1)
            {
                isStopped = 1;
                audio.Stop();

                Save();
            }
        }



    }


    private void Load()
    {
        var data = SaveManager.Load<SoundPropreties>(saveKey);
        volume = data.volume;
        isStopped = data.isStopped;
    }

    private void Save()
    {
        SaveManager.Save(saveKey, GetSaveSnapshot());
    }

    private SoundPropreties GetSaveSnapshot()
    {
        var data = new SoundPropreties()
        {
            volume = this.volume,
            isStopped = this.isStopped,
        };
        return data;
    }
}
