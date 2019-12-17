using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerySimplePistol : MonoBehaviour
{
	public  Transform m_raycastSpot;					
	public  float     m_damage        = 80.0f;				
	public  float     m_forceToApply  = 20.0f;				
	public  float     m_weaponRange   = 9999.0f;						
	public  Texture2D m_crosshairTexture;					
    public  AudioClip m_fireSound;							
    private bool      m_canShot;

	private void Update()
	{
        if (m_canShot)
		{
			if (Input.GetButton("Fire1"))
			{
				Shot();
			}
		}
		else if (Input.GetButtonUp("Fire1"))
        { 
			m_canShot = true;
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

		Ray ray = new Ray(m_raycastSpot.position, m_raycastSpot.forward);

        RaycastHit hit;

		if (Physics.Raycast(ray, out hit, m_weaponRange))
		{
            Debug.Log("Hit " + hit.transform.name);
            if (hit.rigidbody)
			{
				hit.rigidbody.AddForce(ray.direction * m_forceToApply);
                Debug.Log("Hit");
			}
		}

		GetComponent<AudioSource>().PlayOneShot(m_fireSound);
	}
}
