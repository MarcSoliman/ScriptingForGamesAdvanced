using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _forceFactor = .10f;

    [SerializeField] private float _damage = 50;

    [SerializeField] private ParticleSystem _impactParticle;
    [SerializeField] private ParticleSystem _enemyImpactParticle;

    [SerializeField] private AudioClip _shootAudio;
    [SerializeField] private AudioClip _impactAudio;

    [SerializeField] private AudioClip _enemyImpactAudio;
    protected GameObject _parentGameObj;


    private void Awake()
    {
        AudioHelper.PlayClip2D(_shootAudio, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        damageable?.OnDamage(_damage);

        if (collision.gameObject.tag == "Enemy")
        {
            AudioHelper.PlayClip2D(_enemyImpactAudio, 1);
            Instantiate(_enemyImpactParticle, transform.position, Quaternion.identity);
        }

        else
        {
            AudioHelper.PlayClip2D(_impactAudio, 1);
            Instantiate(_impactParticle, transform.position, Quaternion.identity);
        }


        gameObject.SetActive(false);

    }
}
