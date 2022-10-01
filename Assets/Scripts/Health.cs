using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health;
    [SerializeField] private ParticleSystem _damageParticles;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private ParticleSystem _killParticles;
    [SerializeField] private AudioClip _killSound;

    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onHeal;


    public float GetHealth { get { return _health; } }


    public void OnDamage(float damage)
    {
        _health -= damage;
        _onDamage?.Invoke();
        if (_health <= 0)
        {
            Kill();

        }
        else
        {
            DamageFeedback();
        }
    }

    public void IncreaseHealth(float healthBonus)
    {
        _health += healthBonus;
        _onHeal?.Invoke();
    }

    public void Kill()
    {
        KillFeedback();
        gameObject.SetActive(false);

    }


    private void DamageFeedback()
    {
        //particles
        if (_damageParticles != null)
        {
            _damageParticles = Instantiate(_damageParticles, transform.position, Quaternion.identity);
        }
        // audio. TODO - consider Object Pooling
        if (_damageSound != null)
        {
            AudioHelper.PlayClip2D(_damageSound, 1f);
        }


    }

    private void KillFeedback()
    {
        //particles
        if (_killParticles != null)
        {
            _killParticles = Instantiate(_killParticles, transform.position, Quaternion.identity);
        }
        // audio. TODO - consider Object Pooling
        if (_killSound != null)
        {
            AudioHelper.PlayClip2D(_killSound, 1f);
        }


    }
}
