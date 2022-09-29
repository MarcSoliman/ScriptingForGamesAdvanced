using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DamageScreenLogic : MonoBehaviour
{
    private Color _color;
    //get reference to the UI image
    [SerializeField] private Image _image;
    // Start is called before the first frame update
    private void Awake()
    {
        _color = _image.color;
        _color.a = 0;
    }


    public void OnDamage()
    {
        _color.a = 1;
        _image.color = _color;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.5f);
        while (_color.a > 0)
        {
            _color.a -= Time.deltaTime;
            _image.color = _color;
            yield return null;
        }
    }
}
