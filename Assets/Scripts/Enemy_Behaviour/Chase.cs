using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent m_agent;
    private Transform m_playerTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_agent = animator.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_agent.isStopped = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float hearingDistance = Vector3.Distance(animator.transform.position, m_playerTransform.position);
        if (Vector3.Distance(animator.transform.position, m_playerTransform.position) <= 60)
        {
            animator.SetTrigger("Attack");
        }
        if (hearingDistance > 120 || Vector3.Distance(animator.transform.position, m_playerTransform.position) > 60)
        {
            animator.SetTrigger("Idle");
        }
        m_agent.SetDestination(m_playerTransform.position);
    }
}
