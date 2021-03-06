﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class WeaponItem
{
    public string m_name;
    public float m_damage = 80.0f;
    public float m_forceToApply = 1000.0f;
    public float m_weaponRange = 999.0f;
    public int m_ammoCapacity = 10;
    public float m_rateOfShot = 0.5f;
    public float m_accuary = 70.0f;
    public float m_accuaryDropPerShot = 10.0f;
    public float m_accuaryRecovery = 1.0f;
    public int m_simultaneousShots = 1;
    public float m_recoilBack = 0.1f;
    public float m_recoilRecovery = 0.01f;
    public bool m_isAMachineGun = false;
    public bool m_isARocketLauncher = false;

}
