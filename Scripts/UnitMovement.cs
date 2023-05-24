using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovement : MonoBehaviour
{
    private float speed = 1.75f;
    private List<GameObject> movingCharacters = new List<GameObject>();
    private Dictionary<GameObject, Vector3> targetPositions = new Dictionary<GameObject, Vector3>();
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }
        UpdateCharacterMovements();
    }
    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 offset = new Vector3(1f, 0f, 1f); // Mesafe ofseti
            Vector3 spawnPosition = hit.point;
            foreach (var unit in UnitSelection.unitSelected)
            {
                if (!targetPositions.ContainsKey(unit.gameObject))
                {
                    targetPositions.Add(unit.gameObject, spawnPosition);
                    movingCharacters.Add(unit.gameObject);
                }
                else
                {
                    targetPositions[unit.gameObject] = spawnPosition;
                }
                RotateCharacterTowardsTarget(unit.gameObject.transform, spawnPosition);
                unit.GetComponent<Animator>().SetBool("Walk", true);
                spawnPosition += offset;
            }
        }
    }
    private void UpdateCharacterMovements()
    {
        List<GameObject> charactersToRemove = new List<GameObject>();
        foreach (var character in movingCharacters)
        {
            Vector3 targetPosition = targetPositions[character];
            Transform characterTransform = character.transform;
            Animator characterAnimator = character.GetComponent<Animator>();
            if (Vector3.Distance(characterTransform.position, targetPosition) > 0.1f)
            {
                float step = speed * Time.deltaTime;
                characterTransform.position = Vector3.MoveTowards(characterTransform.position, targetPosition, step);
                characterAnimator.SetBool("Walk", true);
            }
            else
            {
                characterAnimator.SetBool("Walk", false);
                charactersToRemove.Add(character);
            }
        }
        foreach (var characterToRemove in charactersToRemove)
        {
            movingCharacters.Remove(characterToRemove);
            targetPositions.Remove(characterToRemove);
        }
        if (movingCharacters.Count == 0)
        {
            animator.SetBool("Walk", false);
        }
    }
    private void RotateCharacterTowardsTarget(Transform character, Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - character.position).normalized;
        character.rotation = Quaternion.LookRotation(direction);
    }
}



