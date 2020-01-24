using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerySimplePistol : MonoBehaviour
{
    public string Name;
    public  Transform m_raycastSpot;										
	public  Texture2D m_crosshairTexture;												
    private bool      m_canShot;
    private int       m_currentAmmo;
    public Text ammoDisplay;
    public Text maxAmmoDisplay;
    private float     m_timer         = 1;

    //ROCKET
    public GameObject m_rocket;
    public Transform m_rocketSpot;
    public GameObject m_weapon;
    private float m_accuracy          = 100;


    [SerializeField]
    private WeaponItem data;
    public WeaponList dataBorrar;

    private void OnEnable()
    {
        for (int i = 0; i < dataBorrar.weaponList.Count; i++)
        {
            if (dataBorrar.weaponList[i].name.Equals(Name))
            {
                data = dataBorrar.weaponList[i];
            }
        }
        m_currentAmmo = data.m_ammoCapacity;
    }

    private void Update()
	{
        //SI ES AMETRALLADORA
        if (data.m_isAMachineGun)
        {
            m_canShot = true;
        }

        ammoDisplay.text    = m_currentAmmo.ToString();
        maxAmmoDisplay.text = data.m_ammoCapacity.ToString();

        m_timer += Time.deltaTime;

        m_accuracy = Mathf.Lerp(data.m_accuary, m_accuracy, data.m_recoilRecovery * Time.deltaTime);
        Debug.DrawRay(m_raycastSpot.position, m_raycastSpot.forward, Color.red);
        //RECOIL RECOVERY
        m_weapon.transform.position = Vector3.Lerp(m_weapon.transform.position,this.transform.position, data.m_recoilRecovery * Time.deltaTime);
        //Disparar
        if (m_canShot && m_currentAmmo > 0 && m_timer > data.m_rateOfShot)
		{
			if (Input.GetButton("Fire1"))
			{
                if (data.m_isARocketLauncher)
                {
                    ShotRocket();
                }
                else
                {
                    Shot();
                }
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
        m_timer = 0;
        for (int i = 0; i < data.m_simultaneousShots; i++)
        {
            Debug.Log("DISPARO");
            float accuaryModifier = (100 - data.m_accuary) / 1000;
            Vector3 directionForward = m_raycastSpot.forward;
            directionForward.x += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
            directionForward.y += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
            directionForward.z += UnityEngine.Random.Range(-accuaryModifier, accuaryModifier);
            m_accuracy -= data.m_accuaryDropPerShot;
            m_accuracy = Mathf.Clamp(data.m_accuary, 0, 100);

            m_weapon.transform.Translate(new Vector3(0, 0, data.m_recoilBack), Space.Self);

            Ray ray = new Ray(m_raycastSpot.position, directionForward);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, data.m_weaponRange))
            {
                Debug.Log("Hit " + hit.transform.name);
                if (hit.collider.tag == "Enemy")
                {
                    //Si es enemigo hacer daño
                    hit.collider.GetComponent<Enemy_SM>().ApplyDamage(data.m_damage);
                }
            }
        }
		GetComponent<AudioSource>().PlayOneShot(data.m_fireSound);
	}

    private void ShotRocket()
    {
        m_timer = 0;
        if (m_currentAmmo <= 0)
        {
            return;
        }

        m_currentAmmo--;

        m_canShot = false;

        Quaternion rotation = Quaternion.Euler(0,0,0);

        for (int i = 0; i < data.m_simultaneousShots; i++)
        {
            GameObject Rocket = Instantiate(m_rocket, m_rocketSpot.position, rotation) as GameObject;
        }
        m_weapon.transform.Translate(new Vector3(0,0,data.m_recoilBack), Space.Self);
        GetComponent<AudioSource>().PlayOneShot(data.m_fireSound);
    }
}
