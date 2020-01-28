using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody m_rgRocket;
    public float m_rocketSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_rgRocket = GetComponent<Rigidbody>();

        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        m_rgRocket.AddForce(-transform.forward * m_rocketSpeed, ForceMode.Force);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy_SM>().ApplyDamage(300);
        }
        Destroy(this.gameObject);
    }
}
