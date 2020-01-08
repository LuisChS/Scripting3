using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent m_agent;
    private Transform m_playerTransform;
    private float timer;
    private bool canShoot;
    private float damage = 10;
    private AudioClip fireSound;
    private Transform m_enemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_agent = animator.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        fireSound = animator.transform.GetComponent<Enemy_SM>().m_fireSound;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_agent.isStopped = true;
        m_enemy = animator.transform;
    }

    private void Shoot()
    {
        if (canShoot)
        {
            Debug.Log("EnemyShoot");
            m_enemy.GetComponent<AudioSource>().PlayOneShot(fireSound);
            m_playerTransform.GetComponent<Player>().m_playerHP -= damage;
            Debug.Log(m_playerTransform.GetComponent<Player>().m_playerHP);
            canShoot = false;
        }
        else
        {
            if (timer >= 2)
            {
                timer = 0;
                canShoot = true;
            }else
            {
                timer += Time.deltaTime;
            }
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float hearingDistance = Vector3.Distance(animator.transform.position, m_playerTransform.position);

        if (hearingDistance > 50)
        {
            animator.SetTrigger("Chase");
        }
        //Disparar
        Shoot();

    }
}
