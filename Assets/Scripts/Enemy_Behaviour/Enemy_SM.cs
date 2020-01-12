using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SM : MonoBehaviour
{
    Animator m_animator;
    private float m_HP = 100.0f;
    public AudioClip m_fireSound;
    private float m_stormDamage;
    private bool m_enemyOut;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        stormDamage();
        if (m_HP<=0){
            Destroy(gameObject);
        }
    }
    public void OnIdleAnimCompleted()
    {
        m_animator.SetTrigger("Patrol");
    }
    public void ApplyDamage(float damage)
    {
        m_HP -= damage/2;
    }
    public void enemyOutStorm (float damage)
    {
        m_stormDamage = damage;
        m_enemyOut = true;
    }
    public void enemyInStorm()
    {
        m_enemyOut = false;
    }
    private void stormDamage()
    {
        if (m_enemyOut)
        {
            m_HP -= m_stormDamage * Time.deltaTime;
        }
    }
}
