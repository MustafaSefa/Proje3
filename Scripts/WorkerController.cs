using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Playables;
public class WorkerController : MonoBehaviour
{
   // public GameObject pickaxe;
    //public GameObject axe;
    //public GameObject hammer;
    //public Animator animator;
public LayerMask unitLayerMask;  // Birimlerin yer aldığı layer
    public float moveSpeed = 2f;  // Birimin hareket hızı
    public float rotateSpeed = 5f;  // Birimin dönüş hızı

    private Camera mainCamera;
    private GameObject selectedUnit;
    private bool isMoving = false;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Birim seçimi için sol fare düğmesini tıklayın
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, unitLayerMask))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Seçili birim varsa, hareketi iptal et
                if (selectedUnit != null)
                {
                    isMoving = false;
                    selectedUnit = null;
                }

                // Birimi seç
                selectedUnit = clickedObject;
                Debug.Log("Selected Unit: " + clickedObject.name);
            }
            else
            {
                // Boş bir yere tıklandıysa, birimin hareketini başlat
                if (selectedUnit != null)
                {
                    isMoving = true;
                    MoveUnitToDestination(hit.point);
                }
            }
        }

        // Birimin hareket etmesi
        if (isMoving && selectedUnit != null)
        {
            Vector3 destination = selectedUnit.transform.position;
            MoveToDestination(selectedUnit.transform, destination);
        }
    }

    void MoveUnitToDestination(Vector3 destination)
    {
        isMoving = true;
        MoveToDestination(selectedUnit.transform, destination);
    }

    void MoveToDestination(Transform unitTransform, Vector3 destination)
    {
        Vector3 startPosition = unitTransform.position;
        float distance = Vector3.Distance(startPosition, destination);
        float elapsedTime = 0f;

        Vector3 direction = (destination - startPosition).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        unitTransform.rotation = targetRotation;

        while (elapsedTime < distance / moveSpeed)
        {
            unitTransform.position = Vector3.Lerp(startPosition, destination, elapsedTime * moveSpeed / distance);
            elapsedTime += Time.deltaTime;

            if (!isMoving)
                return;

            // Başka bir yere tıklanırsa, hareketi iptal et
            if (Input.GetMouseButtonDown(0))
            {
                isMoving = false;
                return;
            }
        }

        unitTransform.position = destination;
        Debug.Log("Unit has reached the destination.");

        isMoving = false;
    }
}
