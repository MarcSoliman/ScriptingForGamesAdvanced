using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingGate : MonoBehaviour
{
    private bool _closed = false;

    public void CloseGate()
    {
        if (_closed) return;
        _closed = true;
        StartCoroutine(CloseGateCoroutine());

    }

    IEnumerator CloseGateCoroutine()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, 11.3f, transform.position.z), 2 * Time.deltaTime);
        yield return new WaitForSeconds(3f);

    }
}
