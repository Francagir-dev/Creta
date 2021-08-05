using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    private float m_VFXVolume, m_BackgroundVolume, m_MasterVolume;

    public void SetSfxLevel(float sfxLvl)
    {
        masterMixer.SetFloat("vfxVolume", sfxLvl);
    }

    public void SetMasterLevel(float musicLvl)
    {
        masterMixer.SetFloat("masterVolume", musicLvl);
    }

    public void SetBackgroundLevel(float backgroundLvL)
    {
        masterMixer.SetFloat("bgVolume", backgroundLvL);
    }
}