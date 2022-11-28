using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public Slider musicSlider;
    public Slider effectSlider;

    public GameObject skinMenu;

    public void Start()
    {
        skinMenu.SetActive(false);
        //on récupere le volume de base pour mettre ajour le slider dans les option
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        audioMixer.GetFloat("SFX", out float effectValueForSlider);
        musicSlider.value = effectValueForSlider;

        //on récupère la résolution de l'écran du joueur
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        //on créer une liste des différentes options de résolution
        List<string> options = new List<string>();

        int curentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                curentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = curentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //on force le jeu a se lancer en full screen
        Screen.fullScreen = true;
    }

    //fonction permettant de changer le volume de la music
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    //fonction permettant de changer le volume des sound effect
    public void SetSoundEffect(float volume)
    {
        audioMixer.SetFloat("SoundEffect", volume);
    }

    //fonction permettant de passer en full screen
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }


    //fonction permettant de changer la résolution du jeu
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
