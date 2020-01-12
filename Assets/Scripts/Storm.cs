using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    Transform m_sphereTransform;
    public float m_stormDamage = 1.0f;
    public float m_sphereDecrement = 1.0f;
    public float m_timerValue = 20.0f;
    private float m_timer;
    private float m_activeTimer = 30.0f;
    // Start is called before the first frame update
    void Start()
    {
        m_sphereTransform = GetComponent<Transform>();
        m_timer = m_timerValue;
    }

    // Update is called once per frame
    void Update()
    {
        StormBehaviour();
    }
    void StormBehaviour()
    {
        Debug.Log(m_timer);
        if (m_timer <= 0)
        {

            if (m_activeTimer >= 0)
            {
                m_activeTimer -= 1 * Time.deltaTime;
                m_sphereTransform.localScale = new Vector3(m_sphereTransform.localScale.x - m_sphereDecrement * Time.deltaTime, m_sphereTransform.localScale.y - m_sphereDecrement * Time.deltaTime, m_sphereTransform.localScale.z - m_sphereDecrement * Time.deltaTime);
            }
            else
            {
                m_sphereDecrement += 3;
                m_stormDamage += 5;
                m_timer = m_timerValue;
            }
        }
        else
        {
            m_timer -= 1 * Time.deltaTime;
            m_activeTimer = 30.0f;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy_SM enemyScript = other.gameObject.GetComponent<Enemy_SM>();
            Debug.Log("Enemigo fuera del circulo");
            if (enemyScript != null)
            {
                enemyScript.enemyOutStorm(m_stormDamage);
            }
        }
        if (other.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            Debug.Log("Jugador fuera del circulo");
            if (playerScript != null)
            {
                playerScript.playerOutStorm(m_stormDamage);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy_SM enemyScript = other.gameObject.GetComponent<Enemy_SM>();
            Debug.Log("Enemigo fuera del circulo");
            if (enemyScript != null)
            {
                enemyScript.enemyInStorm();
            }
        }
        if (other.tag == "Player")
        {
            Player playerScript = other.gameObject.GetComponent<Player>();
            Debug.Log("Jugador fuera del circulo");
            if (playerScript != null)
            {
                playerScript.playerInStorm();
            }
        }
    }
}
