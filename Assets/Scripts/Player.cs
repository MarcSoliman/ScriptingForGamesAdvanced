using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private UnityEvent enteredCamTriggerFar, enteredCamTriggerNear, leftCamTrigger;
    private bool _isInvincible = false;

    public bool IsInvincible
    {
        get => _isInvincible;
        set => _isInvincible = value;
    }

    private TankController _tankController;


    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OffsetTriggerFar"))
        {
            enteredCamTriggerFar?.Invoke();
        }

        if (other.CompareTag("OffsetTriggerNear"))
        {
            enteredCamTriggerNear?.Invoke();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("OffsetTriggerFar") || other.CompareTag("OffsetTriggerNear"))
        {
            leftCamTrigger?.Invoke();
        }
    }

}
