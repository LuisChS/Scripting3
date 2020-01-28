using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : StateMachineBehaviour
{
    Animator m_animator;
    private UnityEngine.AI.NavMeshAgent m_agent;
    private Transform m_playerTransform;
    private Player m_playerBehaviour;
    private bool inSight = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_animator = animator;
        m_agent = animator.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_agent.isStopped = true;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_playerBehaviour = m_playerTransform.GetComponent<Player>();
    }

    float GetDistance()
    {
        float noise = m_playerBehaviour.GetLevelNoise();
        if (noise == 0)
        {
            //Idle
            return 0;
        }
        else if (noise < 2)
        {
            //Andar
            return 10;
        }
        else
        {
            //Correr o atacar
            return float.MaxValue;
        }
    }
    float GetVisionDistance()
    {
        float levelDistance = m_playerBehaviour.GetLevelVision();
        if (levelDistance > 5)
        {
            //Jugador a la luz
            return 10;
        }
        else
        {
            //Jugador en la oscuridad
            return 0;
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Oido
        float hearingDistance = GetDistance();
        if (Vector3.Distance(animator.transform.position, m_playerTransform.position) < hearingDistance)
        {
            animator.SetTrigger("Chase");
        }

        //Vista
        float seeingDistance = GetVisionDistance();
        float Angle = Mathf.Abs(Vector3.Angle(animator.transform.forward, (m_playerTransform.position - animator.transform.position).normalized));
        if (Angle < 20 && inSight)
        {
            if (Vector3.Distance(animator.transform.position, m_playerTransform.position) > seeingDistance)
            {
                animator.SetTrigger("Chase");
            }
        }
        if (Physics.Linecast(animator.transform.position, m_playerTransform.position))
        {
            Debug.DrawRay(animator.transform.position, m_playerTransform.position);
            inSight = false;
        }
        else
        {
            inSight = true;
        }
    }
}
