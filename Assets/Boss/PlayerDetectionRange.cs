using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionRange : MonoBehaviour
{
    private bool _playerDetected = false;
    public bool PlayerDetected { get { return _playerDetected; } }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") _playerDetected = true;
        print("Player is now detected");
    }
}
