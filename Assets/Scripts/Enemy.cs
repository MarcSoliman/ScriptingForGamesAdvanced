using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private ParticleSystem _impactParticles;
    [SerializeField] private AudioClip _impactSound;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null)
        {
            PlayerImpact(player);
            ImpactFeedback();
        }
    }

    protected virtual void PlayerImpact(Player player)
    {
        if (player.IsInvincible) return;

        player.GetComponent<IDamageable>().OnDamage(_damageAmount);
    }

    private void ImpactFeedback()
    {
        //particles
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);
        }
        // audio. TODO - consider Object Pooling
        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }


    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {

    }
}
