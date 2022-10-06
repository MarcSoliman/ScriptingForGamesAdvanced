using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetectionRange : MonoBehaviour
{
    private bool _playerDetected = false;
    public bool PlayerDetected { get { return _playerDetected; } }
    [SerializeField] UnityEvent playerDetected;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerDetected = true;
            playerDetected?.Invoke();
        }

    }
}
