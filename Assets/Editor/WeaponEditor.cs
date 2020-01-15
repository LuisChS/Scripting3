using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WeaponEditor : EditorWindow
{
    private WeaponList inventoryItemList;
    private int viewIndex;
    [MenuItem("Window/Weapon Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(WeaponEditor));
    }

    private void OnEnable()
    {
        if (EditorPrefs.HasKey("ObjectPath"))
        {
            string objectPath = "Assets/Scripts/Listas/WeaponList.asset";
            inventoryItemList = AssetDatabase.LoadAssetAtPath(objectPath, typeof (WeaponList)) as WeaponList;
        }

        if (inventoryItemList == null)
        {
            viewIndex = 1;

            WeaponList asset = ScriptableObject.CreateInstance<WeaponList>();
            AssetDatabase.CreateAsset(asset, "Assets/Scripts/WeaponList.asset");
            AssetDatabase.SaveAssets();

            inventoryItemList = asset;

            if (inventoryItemList)
            {
                inventoryItemList.weaponList = new List<WeaponItem>();
                string relPath = AssetDatabase.GetAssetPath(inventoryItemList);
                EditorPrefs.SetString("ObjectPath", relPath);
            }
        }
    }
    private void OnGUI()
    {
        GUILayout.Label("Weapon Editor", EditorStyles.boldLabel);
        GUILayout.Space(10);

        if (inventoryItemList != null)
        {
            PrintTopMenu();
        }
        else
        {
            GUILayout.Space(10);
            GUILayout.Label("Cant load weapon list");
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(inventoryItemList);
        }
    }

    void PrintTopMenu()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("<- Anterior", GUILayout.ExpandWidth(false)))
        {
            if (viewIndex > 1)
                viewIndex--;
        }
        GUILayout.Space(5);
        if (GUILayout.Button ("Siguiente ->", GUILayout.ExpandWidth(false)))
        {
            if (viewIndex < inventoryItemList.weaponList.Count)
            {
                viewIndex++;
            }
        }
        GUILayout.Space(5);
        if (GUILayout.Button("+ Add Weapon", GUILayout.ExpandWidth(false)))
        {
            AddItem();
        }
        GUILayout.Space(5);
        if (GUILayout.Button("- Delete Weapon", GUILayout.ExpandWidth(false)))
        {
            DeleteItem(viewIndex - 1);
        }
        GUILayout.EndHorizontal();
        if (inventoryItemList.weaponList.Count > 0)
        {
            WeaponListMenu();
        }
        else
        {
            GUILayout.Space(10);
            GUILayout.Label("This weapon list is empty");
        }
    }

    void AddItem()
    {
        WeaponItem newItem = new WeaponItem();
        newItem.m_name = "New Weapon";
        inventoryItemList.weaponList.Add(newItem);
        viewIndex = inventoryItemList.weaponList.Count;
    }
    void DeleteItem(int index)
    {
        inventoryItemList.weaponList.RemoveAt(index);
    }

    void WeaponListMenu()
    {
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        viewIndex = Mathf.Clamp(EditorGUILayout.IntField("CurrentWeapon", viewIndex,GUILayout.ExpandWidth(false)), 1 , inventoryItemList.weaponList.Count);
        EditorGUILayout.LabelField("of    " + inventoryItemList.weaponList.Count.ToString() + " weapon ", "", GUILayout.ExpandWidth(false));
        GUILayout.EndHorizontal();
        string[] _choices = new string[inventoryItemList.weaponList.Count];
        for (int i = 0; i < inventoryItemList.weaponList.Count; i++)
        {
            _choices[i] = inventoryItemList.weaponList[i].m_name;
        }
        int _choiceIndex = viewIndex - 1;
        viewIndex = EditorGUILayout.Popup(_choiceIndex, _choices) + 1;

        GUILayout.Space(10);
        inventoryItemList.weaponList[viewIndex - 1].m_name = EditorGUILayout.TextField("Name", inventoryItemList.weaponList[viewIndex-1].m_name);
        inventoryItemList.weaponList[viewIndex - 1].m_damage = EditorGUILayout.FloatField("Damage", inventoryItemList.weaponList[viewIndex - 1].m_damage);
        

        GUILayout.Label("Icon");
        inventoryItemList.weaponList[viewIndex - 1].m_isAMachineGun = (bool)EditorGUILayout.Toggle("m_isAMachineGun", inventoryItemList.weaponList[viewIndex - 1].m_isAMachineGun, GUILayout.ExpandWidth(false));

        if (inventoryItemList.weaponList[viewIndex - 1].m_isAMachineGun)
        {
            inventoryItemList.weaponList[viewIndex - 1].m_name = EditorGUILayout.TextField("Name", inventoryItemList.weaponList[viewIndex - 1].m_name);
            inventoryItemList.weaponList[viewIndex - 1].m_damage = EditorGUILayout.FloatField("Damage", inventoryItemList.weaponList[viewIndex - 1].m_damage);
        }
    }
}
