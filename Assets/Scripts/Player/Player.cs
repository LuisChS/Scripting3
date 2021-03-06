﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float m_levelNoise = 0;
    private float m_levelVision = 0;
    public float m_playerHP = 100;
    private float m_stormDamage;
    private bool m_outOfStorm;

    public GameObject Pistol;
    public GameObject Escopeta;
    public GameObject AK;
    public GameObject RL;
    public float GetLevelNoise()
    {
        return m_levelNoise;
    }
    public float GetLevelVision()
    {
        return m_levelVision;
    }

    // Start is called before the first frame update
    void Start()
    {
        Idle();
        LightExposure();
    }

    // Update is called once per frame
    void Update()
    {
        stormDamage();
        if (m_playerHP <= 0)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Pistol.SetActive(true);
            Escopeta.SetActive(false);
            AK.SetActive(false);
            RL.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Pistol.SetActive(false);
            Escopeta.SetActive(true);
            AK.SetActive(false);
            RL.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Pistol.SetActive(false);
            Escopeta.SetActive(false);
            AK.SetActive(true);
            RL.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Pistol.SetActive(false);
            Escopeta.SetActive(false);
            AK.SetActive(false);
            RL.SetActive(true);
        }
    }
    public void playerOutStorm(float damage)
    {
        m_stormDamage = damage;
        m_outOfStorm = true;
    }
    public void playerInStorm()
    {
        m_outOfStorm = false;
    }
    private void stormDamage()
    {
        if (m_outOfStorm)
        {
            m_playerHP -= m_stormDamage * Time.deltaTime;
        }
    }
    //Nivel de sonido
    void Idle()
    {
        m_levelNoise = 0;
    }
    void Walk()
    {
        m_levelNoise = 1;
    }
    void Run()
    {
        m_levelNoise = 2;
    }
    void Attack()
    {
        m_levelNoise = 3;
    }
    //Nivel de visibilidad
    void LightExposure()
    {
        m_levelVision = 10;
    }
    void DarkExposure()
    {
        m_levelVision = 2;
    }
}
