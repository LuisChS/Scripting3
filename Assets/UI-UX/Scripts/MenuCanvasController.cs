﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuCanvasController : MonoBehaviour
{
    public GameObject m_mainMenu;
    public GameObject m_options;


    public void OnOnePlayer()
    {
        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
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
    }
}
