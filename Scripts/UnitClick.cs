using System.Collections.Generic;
using UnityEngine;
public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public LayerMask clickable;
    public LayerMask ground;
    public GameObject groundMarker;
    void Start()
    {
        myCam = Camera.main;
    }
    private void TargetPoint()
    {
        groundMarker.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {   
            UnitSelection.Instance.DeselectAll();
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else
                {
                    UnitSelection.Instance.ClickSelect(hit.collider.gameObject);
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    UnitSelection.Instance.DeselectAll();
                }
            }
        }
        if (Input.GetMouseButtonDown(1) && UnitSelection.unitSelected.Count > 0)
        {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;
                groundMarker.SetActive(true);
                Invoke("TargetPoint", 0.5f);
            }
        }
    }
}
