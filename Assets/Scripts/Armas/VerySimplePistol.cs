using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerySimplePistol : MonoBehaviour
{
	public  Transform m_raycastSpot;										
	public  Texture2D m_crosshairTexture;												
    private bool      m_canShot;
    private int       m_currentAmmo;
    public Text ammoDisplay;
    public Text maxAmmoDisplay;
    private float     m_timer         = 1;

    //Punteria
    private float m_accuracy          = 100;

    [SerializeField]
    private WeaponItem data;
    public WeaponList dataBorrar;
    public string id;

    private void Start()
    {
        m_currentAmmo = data.m_ammoCapacity;
    }

    private void OnEnable()
    {
        for (int i = 0; i < dataBorrar.weaponList.Count; i++)
        {
            if (dataBorrar.weaponList[i].name.Equals(id))
            {
                data = dataBorrar.weaponList[i];
            }
        }
        m_currentAmmo = data.m_ammoCapacity;
    }

    private void Update()
	{
        ammoDisplay.text    = m_currentAmmo.ToString();
        maxAmmoDisplay.text = data.m_ammoCapacity.ToString();

        m_timer += Time.deltaTime;

        m_accuracy = Mathf.Lerp(data.m_accuary, m_accuracy, data.m_recoilRecovery * Time.deltaTime);
        Debug.DrawRay(m_raycastSpot.position, m_raycastSpot.forward, Color.red);
        //Disparar
        if (m_canShot && m_currentAmmo > 0 && m_timer > data.m_rateOfShot)
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
        if (m_currentAmmo < data.m_ammoCapacity)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GetComponent<AudioSource>().PlayOneShot(data.m_reloadSound);
                m_currentAmmo = data.m_ammoCapacity;
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
        m_currentAmmo -= 1;
        //Punteria
        float accuaryModifier = (100 - data.m_accuary) / 1000;
        Vector3 directionForward = m_raycastSpot.forward;
        directionForward.x += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
        directionForward.y += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
        directionForward.z += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
        m_accuracy -= data.m_accuaryDropPerShot;
        m_accuracy = Mathf.Clamp(data.m_accuary,0,100);

        Ray ray = new Ray(m_raycastSpot.position, directionForward);
        
        RaycastHit hit;

		if (Physics.Raycast(ray, out hit, data.m_weaponRange))
		{
            Debug.Log("Hit " + hit.transform.name);
            if (hit.collider.tag == "Enemy")
			{
                //Si es enemigo hacer daño
                //hit.rigidbody.AddForce(ray.direction * data.m_forceToApply);
                hit.collider.GetComponent<Enemy_SM>().ApplyDamage(data.m_damage);
                Debug.Log("Hit");
			}
		}

		GetComponent<AudioSource>().PlayOneShot(data.m_fireSound);
	}
}
