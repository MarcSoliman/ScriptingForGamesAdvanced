using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _lookAtTarget;



    public void LookAtPlayer(float lookSpeed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_lookAtTarget.position - transform.position), lookSpeed * Time.deltaTime);

    }

    public void LookDown(float lookSpeed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.down), lookSpeed * Time.deltaTime);
    }

    public void LookAway(float lookSpeed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - _lookAtTarget.position), lookSpeed * Time.deltaTime);
    }



}
