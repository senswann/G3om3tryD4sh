using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    //variable stockant l'audioMixer
    public AudioMixer audioMixer;

    //variable des slider de son
    public Slider musicSlider;
    public Slider effectSlider;

    //variable stockant l'UI du menu de skin
    public GameObject skinMenu;

    public void Start()
    {
        skinMenu.SetActive(false);
        //on récupere le volume de base pour mettre ajour le slider dans les option
        audioMixer.GetFloat("Music", out float musicValueForSlider);
        musicSlider.value = musicValueForSlider;

        audioMixer.GetFloat("SFX", out float effectValueForSlider);
        musicSlider.value = effectValueForSlider;

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
    public void SetFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
