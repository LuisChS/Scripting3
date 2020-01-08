using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_SM : MonoBehaviour
{
    Animator m_animator;
    private float m_HP = 100.0f;
    public AudioClip m_fireSound;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
}
