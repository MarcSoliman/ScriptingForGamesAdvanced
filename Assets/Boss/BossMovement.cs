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



    [Header("Movement Variables")]
    [SerializeField] private float _flyHeight = 5f;
    [SerializeField] private float _flySpeed = 4f;
    [SerializeField] private float _fleeSpeed = 2f;
    [SerializeField] private float _followSpeed = 6f;
    private Rigidbody _RB;
    private float elapsed;
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
        elapsed = 0.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        FleePlayer();
        //FlyAttackMovement();
    }

    void FollowPlayer()
    {

        if (!_playerDetection.PlayerDetected) return;

        _lookAtTarget.LookAtPlayer(1);
        _targetPosition = _player.transform;

        elapsed += Time.deltaTime;

        if (elapsed > 1.0f)
        {
            NavMesh.CalculatePath(transform.position, _targetPosition.position, NavMesh.AllAreas, _path);

            _path.GetCornersNonAlloc(_pathPoints);
            print(_pathPoints.Length);
        }


        //move to final point of the navmesh path
        for (int i = 0; i < _pathPoints.Length - 1; i++)
        {
            _RB.MovePosition(Vector3.MoveTowards(transform.position, _pathPoints[i], _followSpeed * Time.deltaTime));

            Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.red);
        }




    }

    void FleePlayer()
    {
        _lookAtTarget.LookAtPlayer(.1f);

        if (!_playerDetection.PlayerDetected) return;

        var _fleePos = (transform.position - _player.transform.position).normalized * 10f;

        elapsed += Time.deltaTime;



        if (elapsed > 1.0f)
        {
            NavMesh.CalculatePath(transform.position, _fleePos, NavMesh.AllAreas, _path);

            _path.GetCornersNonAlloc(_pathPoints);
            print(_pathPoints.Length);
        }

        //move to final point of the navmesh path
        for (int i = 0; i < _pathPoints.Length - 1; i++)
        {
            _RB.MovePosition(Vector3.MoveTowards(transform.position, _pathPoints[i], _followSpeed * Time.deltaTime));

            //Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.red);
        }

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
