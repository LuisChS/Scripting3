using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvasController : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_options;
    public GameObject m_exit;
    public Slider m_musicSlider;
    public Slider m_sfxSlider;

    private void Start()
    {
        MusicManager.Instance.PlayBackgroundMusic(AppSounds.MAIN_TITLE_MUSIC);
    }


    public void OnOnePlayer()
    {
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
        MusicManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);

    }

    public void OnOptions(bool isOptions)
    {
        //Desactivar popup de menu principal
        m_mainMenu.SetActive(!isOptions);
        {
            MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
        }

        //Activar popup de opciones
        m_options.SetActive(isOptions);
        {
            MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
        }


        if (!isOptions)
        {
            MusicManager.Instance.MusicVolumeSave = m_musicSlider.value;
            MusicManager.Instance.SfxVolumeSave = m_sfxSlider.value;

        }
    }

    public void OnMusicValueChanged()
    {
        MusicManager.Instance.MusicVolume = m_musicSlider.value;
    }

    public void OnSfxValueChanged()
    {
        MusicManager.Instance.SfxVolume = m_sfxSlider.value;
    }



    public void OnExit()
    {
        Application.Quit();
        MusicManager.Instance.PlaySound(AppSounds.BUTTON_SFX);
    }

    
}

