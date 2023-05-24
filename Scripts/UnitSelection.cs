using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public static List<GameObject> unitSelected = new List<GameObject>();
    private static UnitSelection _instance;
    public static UnitSelection Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    public void ClickSelect(GameObject unitToAdd)
    {
        DeselectAll();
        unitSelected.Add(unitToAdd);
    }
    public void ShiftClickSelect(GameObject unitToAdd)
    {
        if (unitSelected.Contains(unitToAdd))
        {
            unitSelected.Remove(unitToAdd);
        }
        else
        {
            unitSelected.Add(unitToAdd);
        }
    }
    public void DragSelect(GameObject unitToAdd)
    {
        if (unitSelected.Contains(unitToAdd))
        {
            unitSelected.Remove(unitToAdd);
        }
        else
        {
            unitSelected.Add(unitToAdd);
        }
    }
    public void DeselectAll()
    {
        unitSelected.Clear();
    }
}
