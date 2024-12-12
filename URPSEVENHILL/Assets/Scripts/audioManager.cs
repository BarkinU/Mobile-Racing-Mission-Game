using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class audioManager : MonoBehaviour
{
  
    #region AudioMixerGroup


    public AudioMixer soundMixer;
    private float SFXprefs;
    private float MUSICprefs;
    private float volumeSFX;
    private float volumeMUSIC;
    public Slider musicSlider;
    public Slider sfxSlider;
    
    public void Start(){
        MUSICprefs = PlayerPrefs.GetFloat("music");
        SFXprefs = PlayerPrefs.GetFloat("sfx");

        SetSFXVolume(SFXprefs);
        SetMusicVolume(MUSICprefs);
        sfxSlider.value = SFXprefs;
        musicSlider.value = MUSICprefs;



    }


    public void SetSFXVolume(float volume){
        soundMixer.SetFloat("mySfx", volume);
        volumeSFX = volume;
        PlayerPrefs.SetFloat("sfx",volumeSFX);

    }
    public void SetMusicVolume(float volume){
        soundMixer.SetFloat("myMusic", volume);
        volumeMUSIC = volume;
        PlayerPrefs.SetFloat("music",volumeMUSIC);


    }
    







    #endregion
    

}
