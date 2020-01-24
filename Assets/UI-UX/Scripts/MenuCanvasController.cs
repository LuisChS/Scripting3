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


    public void OnOnePlayer()
    {
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
        //Debug.Log("Play");
    }

    public void OnOptions(bool isOptions)
    {
        //Desactivar popup de menu principal
        m_mainMenu.SetActive(!isOptions);

        //Activar popup de opciones
        m_options.SetActive(isOptions);
    }

    public void OnExit()
    {
        Application.Quit();
        //Debug.Log("Cerrar juego");

    }

    public void OnMusicValueChanged()
    {
        MusicManager.Instance.MusicVolume = m_musicSlider.value;
    }

    public void OnSfxValueChanged()
    {
        MusicManager.Instance.MusicVolume = m_sfxSlider.value;
    }
}

