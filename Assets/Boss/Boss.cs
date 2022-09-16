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
    [SerializeField] private Laser _laser;

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
    private bool _shouldCry;

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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        Instantiate(_TearEnemy, new Vector3(transform.position.x - 3f, transform.position.y, transform.position.z - 3f),
        Quaternion.identity);
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

        _laser.FireLaser();

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
        _isLanding = true;
        _laser.gameObject.SetActive(false);
        _shouldFollow = true;

        Debug.Log("Landing");


    }





}
