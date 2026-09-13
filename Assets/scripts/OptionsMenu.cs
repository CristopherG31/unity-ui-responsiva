using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
 
public class OptionsMenu : MonoBehaviour
{
    [Header("Controles de UI (arrástralos aquí)")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Toggle fullscreenToggle;
    public Slider volumeSlider;
 
    private Resolution[] resolutions;
 
    void Start()
    {
        // --- Poblar dropdown de resoluciones disponibles ---
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
 
        List<string> resolutionOptions = new List<string>();
        int currentResolutionIndex = 0;
 
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            resolutionOptions.Add(option);
 
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
 
        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
 
        // --- Poblar dropdown de niveles de calidad gráfica ---
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
 
        // --- Estado inicial de fullscreen y volumen ---
        fullscreenToggle.isOn = Screen.fullScreen;
        volumeSlider.value = AudioListener.volume;
    }
 
    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
 
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
 
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
 
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
 