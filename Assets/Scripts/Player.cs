using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth;
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

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    
    void Update()
    {
        
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _currentHealth += amount;
        Debug.Log("Player's health: " + _currentHealth);
    }
    
    public void DecreaseHealth(int amount)
    {
        if (IsInvincible) return;
        
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);

        if (_currentHealth <= 0 )
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (IsInvincible) return;
        
        gameObject.SetActive(false);
        // play particles
        // play sounds
    }
}
