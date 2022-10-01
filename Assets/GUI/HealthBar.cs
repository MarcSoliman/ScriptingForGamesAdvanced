using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _entity;
    private float _currentHealth;
    private float _maxHealth;

    [SerializeField] private Image _fill;
    // Start is called before the first frame update

    void Start()
    {
        _currentHealth = _entity.GetComponent<Health>().GetHealth;
        _maxHealth = _currentHealth;
    }
    public void UpdateHealth()
    {
        _currentHealth = _entity.GetComponent<Health>().GetHealth;
        print(_currentHealth);

        StartCoroutine(UpdateBar());
    }

    IEnumerator UpdateBar()
    {
        float time = 0;
        float start = _fill.fillAmount;
        float end = _currentHealth / _maxHealth;
        while (time < 1)
        {
            _fill.fillAmount = Mathf.Lerp(start, end, time);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
