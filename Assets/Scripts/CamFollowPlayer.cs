using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _offset = 10f;
    [SerializeField] private float _speed = 2f;

    [SerializeField] private Transform _flyPoint;
    private Quaternion _originalRotation;
    public bool _isFlying = false;

    private void Start()
    {
        _originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_isFlying)
        {
            FlyPoint();
        }
        else
        {
            FollowPlayer();
        }






    }

    private void FollowPlayer()
    {
        //return to original position

        if (_player != null)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 35.5f, _speed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, (new Vector3(_player.position.x, transform.position.y, _player.position.z - _offset)), _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _originalRotation, _speed * Time.deltaTime);

        }
    }
    private void FlyPoint()
    {

        _speed = 1f;
        //change field of view to 60
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, _speed * Time.deltaTime);
        //lerping the position to fly point position
        transform.position = Vector3.Lerp(transform.position, (new Vector3(_flyPoint.position.x, transform.position.y, _flyPoint.position.z)), _speed * Time.deltaTime);

        //lerping the rotation to fly point rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, _flyPoint.rotation, _speed * Time.deltaTime);
    }

    public void IsFlying(bool value)
    {
        _isFlying = value;
    }

    public void SetOffset(float value)
    {
        _offset = value;
    }

}
