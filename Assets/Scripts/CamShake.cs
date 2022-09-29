using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Awake()
    {
        originalPos = transform.position;
    }



    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    //shake camera coroutine
    private IEnumerator Shake()
    {
        float elapsed = 0.0f;
        while (elapsed < 0.5f)
        {
            float x = Random.Range(-.3f, .3f) * 0.5f;
            float y = Random.Range(-.3f, .3f) * 0.5f;
            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPos;
    }
}
