using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset = 10f;


    // Update is called once per frame
    void LateUpdate()
    {
        if (_player != null)
        {
            transform.position = Vector3.Lerp(transform.position, (new Vector3(_player.position.x, transform.position.y, _player.position.z - _offset)), .8f * Time.deltaTime);
        }

    }

}
