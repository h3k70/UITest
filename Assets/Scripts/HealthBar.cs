using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _duration = 100f;

    private Health _health;
    private Slider _slider;
    private float _runningTime;
    private float _maxHealth;
    private float _scalePercentage;
    private Coroutine _changingValueJob;

    private void Awake()
    {
        _health = _gameObject.GetComponent<Health>();
        _slider = GetComponent<Slider>();
        _maxHealth = _health.MaxValue;
        _health.HealthChanged += ChangeValue;
    }

    private void Start()
    {
        _slider.value = _health.Value / _maxHealth;
    }

    private void ChangeValue()
    {
        if (_changingValueJob != null)
            StopCoroutine(_changingValueJob);

        _scalePercentage = _health.Value / _maxHealth;
        _changingValueJob = StartCoroutine(ChangingValue(_scalePercentage));
    }

    private IEnumerator ChangingValue(float targetValue)
    {
        _runningTime = 0;

        while (_slider.value != targetValue)
        {
            _runningTime += Time.deltaTime;
            _slider.value = Mathf.MoveTowards(_slider.value, targetValue, _runningTime / _duration);

            yield return null;
        }
    }
}
