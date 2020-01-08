using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : StateMachineBehaviour
{
    private UnityEngine.AI.NavMeshAgent m_agent;
    private Transform m_playerTransform;
    private Transform[] m_waypoints;
    private Player m_playerBehaviour;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_agent = animator.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_playerBehaviour = m_playerTransform.GetComponent<Player>();

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
        m_waypoints = new Transform[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            m_waypoints[i] = waypoints[i].transform;
        }

        Transform randomWaypoint = m_waypoints[Random.Range(0, m_waypoints.Length)];
        m_agent.isStopped = false;
        m_agent.SetDestination(randomWaypoint.position);
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
            return 0;
        }
        else
        {
            //Jugador en la oscuridad
            return 3;
        }
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float hearingDistance = GetDistance();
        if (Vector3.Distance(animator.transform.position, m_playerTransform.position) < hearingDistance)
        {
            Debug.Log("Me oye");
            animator.SetTrigger("Chase");
        }
        if (!m_agent.pathPending && m_agent.remainingDistance < 1)
        {
            animator.SetTrigger("Idle");
            Debug.Log("Patrol -> Idle");
        }

        //Vista
        float seeingDistance = GetVisionDistance();
        float Angle = Mathf.Abs(Vector3.Angle(animator.transform.forward, (m_playerTransform.position - animator.transform.position).normalized));
        if (Angle < 20)
        {
            if (Vector3.Distance(animator.transform.position, m_playerTransform.position) > seeingDistance)
            {
                Debug.Log("Me ha visto");
                animator.SetTrigger("Chase");
            }
        }
    }
}
