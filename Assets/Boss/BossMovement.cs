using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private PlayerDetectionRange _playerDetection;
    [SerializeField] private GameObject _player;

    private NavMeshAgent _navMeshAgent;

    private Transform _targetPosition;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {

        if (_playerDetection.PlayerDetected)
        {
            //move towards player
            _targetPosition = _player.transform;
            _navMeshAgent.destination = _player.transform.position;
            print("following");

        }

    }

    void FleePlayer()
    {

        if (_playerDetection.PlayerDetected)
        {
            //move away from player
            _targetPosition = _player.transform;
            _navMeshAgent.destination = -_player.transform.position;

        }

    }
}
