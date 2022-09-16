using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
public class RigidNavMeshAgent : MonoBehaviour
{

    NavMeshPath _path;
    Rigidbody _RB;
    LineRenderer _line;
    [SerializeField] private bool _debug;
    private Vector3[] _pathPoints = new Vector3[] { Vector3.zero, Vector3.zero, Vector3.zero };
    private void Start()
    {
        _path = new NavMeshPath();
        _RB = GetComponent<Rigidbody>();
        _line = GetComponent<LineRenderer>();
    }

    public void RigidNavMove(Transform _targetPosition, float _followSpeed)
    {
        _line.SetPosition(0, transform.position);
        NavMesh.CalculatePath(transform.position, _targetPosition.position, NavMesh.AllAreas, _path);

        _path.GetCornersNonAlloc(_pathPoints);
        print(_pathPoints.Length);



        //move to final point of the navmesh path
        for (int i = 0; i < _pathPoints.Length - 1; i++)
        {
            _RB.MovePosition(Vector3.MoveTowards(transform.position, _pathPoints[i], _followSpeed * Time.deltaTime));

            if (_path.corners.Length > 1 && _debug)
            {
                _line.positionCount = _path.corners.Length;
                _line.SetPositions(_path.corners);
            }

        }
    }



    public void RigidNavMove(Vector3 _targetPosition, float _followSpeed)
    {
        _line.SetPosition(0, transform.position);
        NavMesh.CalculatePath(transform.position, _targetPosition, NavMesh.AllAreas, _path);

        _path.GetCornersNonAlloc(_pathPoints);
        print(_pathPoints.Length);



        //move to final point of the navmesh path
        for (int i = 0; i < _pathPoints.Length - 1; i++)
        {
            _RB.MovePosition(Vector3.MoveTowards(transform.position, _pathPoints[i], _followSpeed * Time.deltaTime));

            if (_path.corners.Length > 1 && _debug)
            {
                _line.positionCount = _path.corners.Length;
                _line.SetPositions(_path.corners);
            }

        }
    }


    public void RigidNavHover(Vector3 _targetPosition, float _followSpeed)
    {
        var hoverPos = new Vector3(transform.position.x, 0, transform.position.z);
        _line.SetPosition(0, hoverPos);
        NavMesh.CalculatePath(hoverPos, _targetPosition, NavMesh.AllAreas, _path);

        _path.GetCornersNonAlloc(_pathPoints);




        //move to final point of the navmesh path
        for (int i = 0; i < _pathPoints.Length - 1; i++)
        {
            _RB.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(_pathPoints[i].x, transform.position.y, _pathPoints[i].z), _followSpeed * Time.deltaTime));

            if (_path.corners.Length > 1 && _debug)
            {
                _line.positionCount = _path.corners.Length;
                _line.SetPositions(_path.corners);
            }

        }
    }

    public void RigidNavHover(Transform _targetPosition, float _followSpeed)
    {
        var hoverPos = new Vector3(transform.position.x, 0, transform.position.z);
        _line.SetPosition(0, hoverPos);
        NavMesh.CalculatePath(hoverPos, _targetPosition.position, NavMesh.AllAreas, _path);

        _path.GetCornersNonAlloc(_pathPoints);




        //move to final point of the navmesh path
        for (int i = 0; i < _pathPoints.Length - 1; i++)
        {
            _RB.MovePosition(Vector3.MoveTowards(transform.position, new Vector3(_pathPoints[i].x, transform.position.y, _pathPoints[i].z), _followSpeed * Time.deltaTime));

            if (_path.corners.Length > 1 && _debug)
            {
                _line.positionCount = _path.corners.Length;
                _line.SetPositions(_path.corners);
            }

        }
    }

}
