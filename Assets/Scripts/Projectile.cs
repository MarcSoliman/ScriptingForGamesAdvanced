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

    [SerializeField] private AudioClip _shootAudio;
    [SerializeField] private AudioClip _impactAudio;
    protected GameObject _parentGameObj;


    private void Awake()
    {
        AudioHelper.PlayClip2D(_shootAudio, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.collider.GetComponent<IDamageable>();
        damageable?.OnDamage(_damage);

        AudioHelper.PlayClip2D(_impactAudio, 1);
        Instantiate(_impactParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
