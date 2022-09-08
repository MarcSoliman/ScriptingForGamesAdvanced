using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    
    public void OnDamage(float damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Kill();
        }
    }

    public void IncreaseHealth(float healthBonus)
    {
        _health += healthBonus;
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
