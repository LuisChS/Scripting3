using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum AI_STATE { IDLE, PATROL, CHASE, ATTACK}

public class AI_Behaviour : MonoBehaviour
{

    public AI_STATE currentState = AI_STATE.IDLE;
    private Animator             m_animator;
    private NavMeshAgent         m_agent;
    private Transform            m_playerTransform;
    private Transform            m_transform;
    private BoxCollider          m_collider;
    private bool                 m_canSeePlayerTransform;
    private Transform[]          m_waypointsVector;

    public float ChaseTimeOut = 0;
    public float DistEps = 0;
    public float AttackDelay = 0;
    public float FieldOfView = 0;
    int SightMask;

    void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_transform = transform;
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        m_collider = GetComponent<BoxCollider>();

        GameObject[] waypointsVector = GameObject.FindGameObjectsWithTag("Waypoint");
        m_waypointsVector = new Transform[waypointsVector.Length];
        for (int i = 0; i < waypointsVector.Length; i++)
        {
            m_waypointsVector[i] = waypointsVector[i].transform;
            Debug.Log(m_waypointsVector[i]);
        }

        m_agent.updatePosition = true;
        m_agent.updateRotation = true;
        StartCoroutine(Idle());
    }

    IEnumerator Idle()
    {
        currentState = AI_STATE.IDLE;

        m_animator.SetTrigger("Idle");

        while (currentState == AI_STATE.IDLE)
        {
            if (m_canSeePlayerTransform)
            {
                StartCoroutine(Chase());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Patrol()
    {
        currentState = AI_STATE.PATROL;

        m_animator.SetTrigger("Patrol");
        Debug.Log("Patrol");
        Transform RandomDest = m_waypointsVector[Random.Range(0, m_waypointsVector.Length)];
        m_agent.isStopped = false;
        m_agent.SetDestination(RandomDest.position);

        while (currentState == AI_STATE.PATROL)
        {
            if (m_canSeePlayerTransform)
            {
                StartCoroutine(Chase());
                yield break;
            }

            if (Vector3.Distance(m_transform.position, RandomDest.position) <= DistEps)
            {
                StartCoroutine(Idle());
                yield break;
            }

            yield return null;
        }
    }

    IEnumerator Chase()
    {
        currentState = AI_STATE.CHASE;
        m_animator.SetTrigger("Chase");

        while (currentState == AI_STATE.CHASE)
        {
            m_agent.SetDestination(m_playerTransform.position);

            if (!m_canSeePlayerTransform)
            {
                float ElapsedTime = 0f;

                while (true)
                {
                    ElapsedTime += Time.deltaTime;
                    m_agent.isStopped = false;
                    m_agent.SetDestination(m_playerTransform.position);

                    yield return null;

                    if (ElapsedTime >= ChaseTimeOut)
                    {
                        if (!m_canSeePlayerTransform)
                        {
                            StartCoroutine(Idle());
                            yield break;
                        }
                        else
                            break;
                    }
                }
            }

            if (Vector3.Distance(m_transform.position, m_playerTransform.position) <= DistEps)
            {
                StartCoroutine(Attack());
                yield break;
            }

            yield return null;
        }
    }


    IEnumerator Attack()
    {
        currentState = AI_STATE.ATTACK;

        m_animator.SetTrigger("Attack");
        Debug.Log("Attack");
        float ElapsedTime = 0f;
        m_agent.isStopped = true;
        while (currentState == AI_STATE.ATTACK)
        {
            ElapsedTime += Time.deltaTime;

            if (!m_canSeePlayerTransform || Vector3.Distance(m_transform.position, m_playerTransform.position) > DistEps)
            {
                StartCoroutine(Chase());
                yield break;
            }

            if (ElapsedTime >= AttackDelay)
            {
                ElapsedTime = 0f;

                // Do Damage To Player
            }

            yield return null;
        }
    }

    void Update()
    {
        m_canSeePlayerTransform = false;

        if (!m_collider.bounds.Contains(m_playerTransform.position))
            return;

        m_canSeePlayerTransform = HaveLineSightToPlayer(m_playerTransform);
    }

    private bool HaveLineSightToPlayer(Transform player)
    {
        float Angle = Mathf.Abs(Vector3.Angle(m_transform.forward, (player.position - m_transform.position).normalized));

        if (Angle > FieldOfView)
            return false;

        return true;
    }

    public void OnIdleAnimCompleted()
    {
        StopAllCoroutines();
        StartCoroutine(Patrol());
    }
}
