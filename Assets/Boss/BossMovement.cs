using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerDetectionRange _playerDetection;
    [SerializeField] private LookAtTarget _lookAtTarget;
    [SerializeField] private GameObject _player;
    [SerializeField] private NavMeshPath _path;
    [SerializeField] private RigidNavMeshAgent _rigidNavMeshAgent;


    [Header("Movement Variables")]
    [SerializeField] private float _flyHeight = 5f;
    [SerializeField] private float _flySpeed = 4f;
    [SerializeField] private float _fleeSpeed = 2f;
    [SerializeField] private float _followSpeed = 6f;
    private Rigidbody _RB;

    private Transform _targetPosition;

    private bool _isFlying = false;
    private bool _isLanding = false;

    private Vector3 _landingPosition;

    private Vector3[] _pathPoints = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero };

    private void Awake()
    {
        _RB = GetComponent<Rigidbody>();

    }
    // Start is called before the first frame update
    void Start()
    {
        _path = new NavMeshPath();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //FollowPlayer();
        FleePlayer();
        //FlyAttackMovement();
    }

    void FollowPlayer()
    {

        if (!_playerDetection.PlayerDetected) return;

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

        _isFlying = true;
        _lookAtTarget.LookDown(1f);


        //call Landing() after 5 seconds
        Invoke(nameof(LandingBegin), 5f);
    }

    private void LandingBegin()
    {
        _isLanding = true;
        _lookAtTarget.LookAtPlayer(.2f);

        if (transform.position.y <= 0.5f)
        {
            _isFlying = false;
        }


    }
}
