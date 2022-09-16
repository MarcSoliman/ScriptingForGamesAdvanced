using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDetectionRange _playerDetection;
    [SerializeField] private LookAtTarget _lookAtTarget;
    [SerializeField] private GameObject _player;
    [SerializeField] private NavMeshPath _path;
    [SerializeField] private RigidNavMeshAgent _rigidNavMeshAgent;
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _projectileSpawner;
    [SerializeField] private float _projectileForceFactor = 10;
    [SerializeField] private Laser _laser;
    [SerializeField] private AudioSource _laserSound;
    [SerializeField] private GameObject _TearEnemy;


    [Header("Movement Variables")]
    [SerializeField] private float _flyHeight = 5f;
    [SerializeField] private float _flySpeed = 4f;
    [SerializeField] private float _fleeSpeed = 2f;
    [SerializeField] private float _followSpeed = 6f;
    private Rigidbody _RB;

    private Transform _targetPosition;

    private bool _isFlying = false;
    private bool _isLanding = false;
    float targetTimer = 10f;
    private Vector3 _landingPosition;

    private bool _shouldFollow;
    private bool _shouldFlee;

    private bool _shouldFly;
    private bool _shouldCry = true;

    private bool _shouldShoot = true;

    private Vector3 _originalRotation;

    private Vector3[] _pathPoints = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero };

    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();
        _health = GetComponent<Health>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _path = new NavMeshPath();
        _laser.gameObject.SetActive(false);
        _originalRotation = transform.eulerAngles;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        print(transform.position);
        FollowPlayer();
        FleePlayer();
        FlyAttackMovement();

        if (_health.GetHealth > 7)
        {
            _shouldFollow = true;
            _shouldFlee = false;
            _shouldFly = false;
        }
        else if (_health.GetHealth <= 7 && _health.GetHealth > 5)
        {
            _shouldFly = true;
        }
        else
        {
            _shouldFly = false;
            _shouldFlee = true;
        }
    }

    void FollowPlayer()
    {
        if (!_shouldFollow) return;

        if (!_playerDetection.PlayerDetected) return;

        _RB.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;


        _lookAtTarget.LookAtPlayer(1);
        _targetPosition = _player.transform;


        _rigidNavMeshAgent.RigidNavMove(_targetPosition, _followSpeed);

        var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer < 30 && _shouldShoot)
        {
            StartCoroutine(Shoot(UnityEngine.Random.Range(3, 8)));

        }


    }

    //coroutine to run Shoot
    IEnumerator Shoot(float time)
    {
        _shouldShoot = false;
        yield return new WaitForSeconds(time);
        ShootProjectile();
        _shouldShoot = true;
    }

    private void ShootProjectile()
    {
        var instancedProjectile = Instantiate(_projectile, _projectileSpawner.position, Quaternion.identity);
        instancedProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * _projectileForceFactor, ForceMode.Impulse);
    }

    void FleePlayer()
    {
        if (!_shouldFlee) return;
        _lookAtTarget.LookAtPlayer(.1f);

        if (!_playerDetection.PlayerDetected) return;

        var _fleePos = (transform.position - _player.transform.position).normalized * 10f;

        _rigidNavMeshAgent.RigidNavMove(_fleePos, _followSpeed);

        if (!_shouldCry) return;
        StartCoroutine(ShedTear(4));

    }

    IEnumerator ShedTear(float time)
    {
        _shouldCry = false;
        yield return new WaitForSeconds(time);
        Instantiate(_TearEnemy, new Vector3(transform.position.x + 5, transform.position.y, transform.position.z + 5), Quaternion.identity);

        _shouldCry = true;
    }

    void FlyAttackMovement()
    {

        if (_isLanding || !_shouldFly) return;

        _laser.gameObject.SetActive(true);
        _RB.constraints = RigidbodyConstraints.None;
        _lookAtTarget.LookDown(3f);

        _targetPosition = _player.transform;
        _rigidNavMeshAgent.RigidNavHover(_targetPosition, _flySpeed);

        //add force to rigid body to move it up to 10 y units
        _RB.velocity = new Vector3(_RB.velocity.x, _flyHeight, _RB.velocity.z);

        //move transform yp by flyHeight 
        // transform.position = Vector3.Lerp(transform.position,
        // (new Vector3(transform.position.x, _flyHeight, transform.position.z)),
        //  .8f * Time.deltaTime);

        _laser.FireLaser();
        if (!_laserSound.isPlaying)
        {
            _laserSound.Play();
        }

        StartCoroutine(LandAfterTime(time: 10f));
    }

    //create LandAfterTime coroutine
    IEnumerator LandAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Land();
    }

    private void Land()
    {
        if (_laserSound.isPlaying)
        {
            _laserSound.Stop();
        }
        _isLanding = true;
        _laser.gameObject.SetActive(false);
        _shouldFollow = true;
        //set trotation to original rotation
        transform.eulerAngles = _originalRotation;

        Debug.Log("Landing");
    }





}
