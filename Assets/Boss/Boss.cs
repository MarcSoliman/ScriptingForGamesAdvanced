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

        if (_health.GetHealth > 7)
        {
            FollowPlayer();
        }
        else
        {
            FlyAttackMovement();
        }
    }

    void FollowPlayer()
    {

        if (!_playerDetection.PlayerDetected) return;

        _RB.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;


        _lookAtTarget.LookAtPlayer(1);
        _targetPosition = _player.transform;


        _rigidNavMeshAgent.RigidNavMove(_targetPosition, _followSpeed);


    }

    void FleePlayer()
    {
        _lookAtTarget.LookAtPlayer(.1f);

        if (!_playerDetection.PlayerDetected) return;

        var _fleePos = (transform.position - _player.transform.position).normalized * 10f;



        _rigidNavMeshAgent.RigidNavMove(_fleePos, _followSpeed);



    }

    void FlyAttackMovement()
    {

        if (_isLanding) return;



        _laser.gameObject.SetActive(true);
        _RB.constraints = RigidbodyConstraints.None;
        _lookAtTarget.LookDown(3f);

        _targetPosition = _player.transform;
        _rigidNavMeshAgent.RigidNavHover(_targetPosition, _flySpeed);

        //add force to rigid body to move it up to 10 y units
        _RB.velocity = new Vector3(_RB.velocity.x, _flyHeight, _RB.velocity.z);

        _laser.FireLaser();


        targetTimer -= Time.deltaTime;
        if (targetTimer <= 0)
        {
            targetTimer = 20f;
            Land();

        }



    }

    private void Land()
    {
        _isLanding = true;
        _laser.gameObject.SetActive(false);

        targetTimer -= Time.deltaTime;
        if (targetTimer <= 0)
        {
            _isLanding = false;
            targetTimer = 10f;
            FlyAttackMovement();

        }
    }
}
