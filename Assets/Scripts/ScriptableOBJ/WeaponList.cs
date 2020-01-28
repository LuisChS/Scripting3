﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponList : ScriptableObject
{
    [SerializeField]
    public List<WeaponItem> weaponList;
}
