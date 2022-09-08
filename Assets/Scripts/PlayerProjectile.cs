using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    [SerializeField] private Transform _projectileSpawner;
    [SerializeField] private float _forceFactor = 10;
    [SerializeField] private ParticleSystem _smokeParticles;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        var instancedProjectile = Instantiate(_projectile, _projectileSpawner.position, Quaternion.identity);
        _smokeParticles.Play();
        instancedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * _forceFactor, ForceMode.Impulse);
    }
}
