using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float m_levelNoise = 0;
    private float m_levelVision = 0;
    public float m_playerHP = 100;
    private float m_stormDamage;
    private bool m_outOfStorm;
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
