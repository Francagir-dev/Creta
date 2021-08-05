using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphicOptions : MonoBehaviour
{
    private int width,
        height,
        qualityIndex,
        defaultWidth,
        defaultHeight,
        defaultResolutionResolutionOptionIndex,
        _selectedResolutionResolutionOptionIndex;

    private bool fullScreen, vfx;
    private Resolution[] resolutions;

    [SerializeField] private Toggle vfxToggle, fullScreenToggle;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;

    #region GettersAndSetters

    public int Width
    {
        get => width;
        set => width = value;
    }

    public int Height
    {
        get => height;
        set => height = value;
    }

    public int QualityIndex
    {
        get => qualityIndex;
        set => qualityIndex = value;
    }

    public int DefaultWidth
    {
        get => defaultWidth;
        set => defaultWidth = value;
    }

    public int DefaultHeight
    {
        get => defaultHeight;
        set => defaultHeight = value;
    }

    public int DefaultResolutionOptionIndex
    {
        get => defaultResolutionResolutionOptionIndex;
        set => defaultResolutionResolutionOptionIndex = value;
    }

    public int SelectedResolutionOptionIndex
    {
        get => _selectedResolutionResolutionOptionIndex;
        set => _selectedResolutionResolutionOptionIndex = value;
    }

    public bool FullScreen
    {
        get => fullScreen;
        set => fullScreen = value;
    }

    public bool VFX
    {
        get => vfx;
        set => vfx = value;
    }

    public TMP_Dropdown ResolutionDropdown
    {
        get => resolutionDropdown;
        set => resolutionDropdown = value;
    }

    public TMP_Dropdown QualityDropdown
    {
        get => qualityDropdown;
        set => qualityDropdown = value;
    }

    public Toggle VFXToggle
    {
        get => vfxToggle;
        set => vfxToggle = value;
    }

    public Toggle FullScreenToggle
    {
        get => fullScreenToggle;
        set => fullScreenToggle = value;
    }

    #endregion

    private void Start()
    {
        PopulateDropDownResolutionOptions();
        qualityIndex = 3;
    }

    public void ChangeResolution(int index)
    {
        Resolution resolution = resolutions[index];
        width = resolution.width;
        height = resolution.height;
        _selectedResolutionResolutionOptionIndex = index;
    }


    public void SetQuality(int qualitySettingsIndex)
    {
        qualityIndex = qualitySettingsIndex;
    }

    public void PopulateDropDownResolutionOptions()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        resolutionDropdown.ClearOptions();
        defaultResolutionResolutionOptionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                _selectedResolutionResolutionOptionIndex = i;
                defaultWidth = resolutions[i].width;
                defaultHeight = resolutions[i].height;
                defaultResolutionResolutionOptionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = _selectedResolutionResolutionOptionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void AcceptGraphicsSettings()
    {
        Screen.SetResolution(width, height, fullScreen);
        QualitySettings.SetQualityLevel(qualityIndex);
        CretaPlayerPrefs.instance.SaveGraphicSettings();
    }

    private void OnEnable()
    {
        CretaPlayerPrefs.instance.CheckGraphicsSettings();
    }
    public void ToggleVFX(){}
 }