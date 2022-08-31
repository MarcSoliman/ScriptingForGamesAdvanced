using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Object = UnityEngine.Object;

public abstract class PowerUpBase : MonoBehaviour
{
    [SerializeField] private float _powerupDuration = 3f;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private ParticleSystem _collectParticles;
    [SerializeField] private AudioClip _collectSound;
    private bool _isPowerupActive = false;
    private Collider _collider;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rb;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        
        //If the player is not null and the powerup is not already active do not start
        if (player != null && !_isPowerupActive)  
        {
           PowerUp(player);
           _isPowerupActive = true;
           StartCoroutine(PowerUpTimer(player));
           _collider.enabled = false;
           _meshRenderer.enabled = false;
           Feedback();

        }
    }
    
    //a timer to keep track of power up duration and then call PowerDown() when it's done
    private IEnumerator PowerUpTimer(Player player)
    {
        yield return new WaitForSeconds(_powerupDuration);
        PowerDown(player);
        _isPowerupActive = false;
        gameObject.SetActive(false);
    }
    protected abstract void PowerUp(Player player);
    
    protected abstract void PowerDown(Player player);

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {
        //calculate rotation
        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        //apply rotation
        rb.MoveRotation(rb.rotation * turnOffset);
    }
    
    private void Feedback()
    {
        // spawn particles
        if (_collectParticles != null)
        {
            _collectParticles = Instantiate(_collectParticles, transform.position, Quaternion.identity);
        }
        // play sfx. TODO - consider object pooling for performance
        if (_collectSound != null)
        {
            AudioHelper.PlayClip2D(_collectSound, 1f);
        }
    }
}
