using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerySimplePistol : MonoBehaviour
{
	public  Transform m_raycastSpot;					
	public  float     m_damage        = 80.0f;				
	public  float     m_forceToApply  = 20.0f;				
	public  float     m_weaponRange   = 9999.0f;						
	public  Texture2D m_crosshairTexture;					
    public  AudioClip m_fireSound;							
    private bool      m_canShot;
    //Variables municion
    private int       m_Ammo;
    public int        m_MaxAmmo       = 20;
    public AudioClip  m_reloadSound;

    public Text ammoDisplay;
    public Text maxAmmoDisplay;
    //Ratio de disparo
    public float      m_bulletsPerSecond = 0.5f;
    private float     m_timer         = 1;

    //Punteria
    private float m_accuracy          = 100;
    private float m_currentAccuracy   = 100;
    private float m_accuracyDropPerShot = 10;
    private float m_accuracyRecoverPerSecond = 0.1f;
    private void Start()
    {
        m_Ammo = m_MaxAmmo;
    }
    private void Update()
	{
        ammoDisplay.text    = m_Ammo.ToString();
        maxAmmoDisplay.text = m_MaxAmmo.ToString();

        m_timer += Time.deltaTime;

        m_currentAccuracy = Mathf.Lerp(m_currentAccuracy, m_accuracy, m_accuracyRecoverPerSecond * Time.deltaTime);
        Debug.DrawRay(m_raycastSpot.position, m_raycastSpot.forward, Color.red);
        //Disparar
        if (m_canShot && m_Ammo > 0 && m_timer > m_bulletsPerSecond)
		{
			if (Input.GetButton("Fire1"))
			{
                m_timer = 0;
				Shot();
			}
		}
		else if (Input.GetButtonUp("Fire1"))
        { 
			m_canShot = true;
        }
        //Recargar
        if (m_Ammo < m_MaxAmmo)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                m_Ammo = m_MaxAmmo;
            }
        }
    }

	private void OnGUI()
	{
		Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
		Rect auxRect = new Rect(center.x - 20, center.y - 20, 20, 20);
		GUI.DrawTexture(auxRect, m_crosshairTexture, ScaleMode.StretchToFill);
	}


	private void Shot()
	{
		m_canShot = false;
        m_Ammo -= 1;
        //Punteria
        float accuaryModifier = (100 - m_currentAccuracy) / 1000;
        Vector3 directionForward = m_raycastSpot.forward;
        directionForward.x += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
        directionForward.y += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
        directionForward.z += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
        m_currentAccuracy -= m_accuracyDropPerShot;
        m_currentAccuracy = Mathf.Clamp(m_currentAccuracy,0,100);

        Ray ray = new Ray(m_raycastSpot.position, directionForward);
        
        RaycastHit hit;

		if (Physics.Raycast(ray, out hit, m_weaponRange))
		{
            Debug.Log("Hit " + hit.transform.name);
            if (hit.rigidbody)
			{
                //Si es enemigo hacer daño
				hit.rigidbody.AddForce(ray.direction * m_forceToApply);
                Debug.Log("Hit");
			}
		}

		GetComponent<AudioSource>().PlayOneShot(m_fireSound);
	}
}
