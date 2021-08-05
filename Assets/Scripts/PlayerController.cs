using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector3 m_RawInputMovement;
   [SerializeField] private float customSpeed = 1.2f;

    private void Update()
    {
        transform.Translate(m_RawInputMovement * (Time.deltaTime * customSpeed));
    }

    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();

        m_RawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y).normalized;
    }
}