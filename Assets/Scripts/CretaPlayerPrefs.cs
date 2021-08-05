using System;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CretaPlayerPrefs : MonoBehaviour
{
    [Header("SOUND")] [SerializeField] private Slider bgSound;
    [SerializeField] private Slider masterSound;
    [SerializeField] private Slider vfxSound;
    [SerializeField] private AudioMixer masterMixer;

    [Header("GRAPHICS")] [SerializeField] private TMP_Dropdown resolution;

    [FormerlySerializedAs("m_GraphicOptions")] [SerializeField]
    private GraphicOptions graphicOptions;

    public static CretaPlayerPrefs instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region SoundSettings

    /// <summary>
    /// Save in PlayerPrefs the value of sound
    /// </summary>
    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(Constants.VFXVolumePref, vfxSound.value);
        PlayerPrefs.SetFloat(Constants.MasterVolumePref, masterSound.value);
        PlayerPrefs.SetFloat(Constants.BgVolumePref, bgSound.value);
    }

    /// <summary>
    /// Will check if any of this sounds are saved in PlayerPrefs
    /// </summary>
    public void CheckSoundSettings()
    {
        //Background
        if (PlayerPrefs.HasKey(Constants.BgVolumePref))
        {
            bgSound.value = PlayerPrefs.GetFloat(Constants.BgVolumePref);
            masterMixer.SetFloat("bgVolume", PlayerPrefs.GetFloat(Constants.BgVolumePref));
        }
        else
        {
            bgSound.value = 1.0f;
            masterMixer.SetFloat("bgVolume", bgSound.value);
            PlayerPrefs.SetFloat(Constants.BgVolumePref, bgSound.value);
        }

        // MASTER
        if (PlayerPrefs.HasKey(Constants.MasterVolumePref))
        {
            masterSound.value = PlayerPrefs.GetFloat(Constants.MasterVolumePref);
            masterMixer.SetFloat("masterVolume", PlayerPrefs.GetFloat(Constants.MasterVolumePref));
        }
        else
        {
            masterSound.value = 1.0f;
            masterMixer.SetFloat("masterVolume", masterSound.value);
            PlayerPrefs.SetFloat(Constants.MasterVolumePref, masterSound.value);
        }

        //VFX
        if (PlayerPrefs.HasKey(Constants.VFXVolumePref))
        {
            vfxSound.value = PlayerPrefs.GetFloat(Constants.VFXVolumePref);
            masterMixer.SetFloat("vfxVolume", PlayerPrefs.GetFloat(Constants.VFXVolumePref));
        }
        else
        {
            vfxSound.value = 1.0f;
            masterMixer.SetFloat("vfxVolume", vfxSound.value);
            PlayerPrefs.SetFloat(Constants.VFXVolumePref, vfxSound.value);
        }
    }

    #endregion

    #region GraphicSettings

    public void SaveGraphicSettings()
    {
        PlayerPrefs.SetInt(Constants.WidthGraphicsPref, graphicOptions.Width);
        PlayerPrefs.SetInt(Constants.HeightGraphicsPref, graphicOptions.Height);
        PlayerPrefs.SetInt(Constants.VFXGraphicsPref, BoolToInt(graphicOptions.VFX));
        PlayerPrefs.SetInt(Constants.FullscreenGraphicsPref, BoolToInt(graphicOptions.FullScreen));
        PlayerPrefs.SetInt(Constants.SelectedResolutionIndexGraphicsPref, graphicOptions.SelectedResolutionOptionIndex);
        PlayerPrefs.SetInt(Constants.QualityIndexGraphicsPref, graphicOptions.QualityIndex);
    }

    public void CheckGraphicsSettings()
    {
        graphicOptions.PopulateDropDownResolutionOptions();
        //Width
        if (PlayerPrefs.HasKey(Constants.WidthGraphicsPref))
        {
            graphicOptions.Width = PlayerPrefs.GetInt(Constants.WidthGraphicsPref);
            Debug.Log("Width of screen will be: " + graphicOptions.Width);
        }
        else
        {
            graphicOptions.Width = graphicOptions.DefaultWidth;
            PlayerPrefs.SetInt(Constants.WidthGraphicsPref, graphicOptions.DefaultWidth);
            Debug.Log("Width of screen will be: " + graphicOptions.Width);
            Debug.Log("Default Width of screen will be: " + graphicOptions.Width);
        }

        //Height
        if (PlayerPrefs.HasKey(Constants.HeightGraphicsPref))
        {
            graphicOptions.Height = PlayerPrefs.GetInt(Constants.HeightGraphicsPref);
            Debug.Log("Width of screen will be: " + graphicOptions.Height);
        }
        else
        {
            graphicOptions.Width = graphicOptions.DefaultHeight;
            PlayerPrefs.SetInt(Constants.HeightGraphicsPref, graphicOptions.DefaultHeight);
            Debug.Log("Height of screen will be: " + graphicOptions.Height);
            Debug.Log("Default Height of screen will be: " + graphicOptions.Height);
        }

        //vfx
        if (PlayerPrefs.HasKey(Constants.VFXGraphicsPref))
        {
            //in next line (not active for now) will activate or disable all VFX
            //graphicOptions.ToggleVFX();
            graphicOptions.VFX = PlayerPrefs.GetInt(Constants.VFXGraphicsPref) == 1;
            graphicOptions.VFXToggle.isOn = graphicOptions.VFX;
            Debug.Log("Player want VFX?" + graphicOptions.VFXToggle.isOn);
        }
        else
        {
            graphicOptions.VFX = true;
            graphicOptions.VFXToggle.isOn = true;
            PlayerPrefs.SetInt(Constants.VFXGraphicsPref, 1);
            Debug.Log("Player want VFX?" +  graphicOptions.VFX);
        }

        //FullScreen
        if (PlayerPrefs.HasKey(Constants.FullscreenGraphicsPref))
        {
            graphicOptions.FullScreen = PlayerPrefs.GetInt(Constants.FullscreenGraphicsPref) == 1;
            graphicOptions.FullScreenToggle.isOn = graphicOptions.FullScreen;
            Debug.Log("Want Full Screen? "+graphicOptions.FullScreen);
        }
        else
        {
            graphicOptions.FullScreen = true;
            graphicOptions.FullScreenToggle.isOn = true;
            PlayerPrefs.SetInt(Constants.FullscreenGraphicsPref, 1);
            Debug.Log("set Full Screen? "+graphicOptions.FullScreen);
        }

        //Dropdown Resolution
        if (PlayerPrefs.HasKey(Constants.SelectedResolutionIndexGraphicsPref))
        {
            graphicOptions.SelectedResolutionOptionIndex = PlayerPrefs.GetInt(Constants.SelectedResolutionIndexGraphicsPref);
            graphicOptions.ResolutionDropdown.value = graphicOptions.SelectedResolutionOptionIndex;
            Debug.Log("Default ResolutionDropdown :"+ graphicOptions.DefaultResolutionOptionIndex+" selected " );
        }
        else
        {
            graphicOptions.SelectedResolutionOptionIndex = graphicOptions.DefaultResolutionOptionIndex;
            graphicOptions.ResolutionDropdown.value = graphicOptions.DefaultResolutionOptionIndex;
        }

        //Dropdown Quality
        if (PlayerPrefs.HasKey(Constants.QualityIndexGraphicsPref))
        {
            graphicOptions.QualityIndex = PlayerPrefs.GetInt(Constants.QualityIndexGraphicsPref);
            graphicOptions.QualityDropdown.value = PlayerPrefs.GetInt(Constants.QualityIndexGraphicsPref);
        }
        else
        {
            graphicOptions.QualityIndex = 2;
            graphicOptions.QualityDropdown.value = 2;
        }

        Screen.SetResolution(graphicOptions.Width, graphicOptions.Height, graphicOptions.FullScreen);
        QualitySettings.SetQualityLevel(graphicOptions.QualityIndex);
    }

    private int BoolToInt(bool option)
    {
        return option ? 1 : 0;
    }

    #endregion
    
}
