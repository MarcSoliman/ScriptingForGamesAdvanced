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


    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
