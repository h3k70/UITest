using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private UnityEvent _healthChanged;

    public float Value => _health;
    public float MaxValue => _maxHealth;

    public event UnityAction HealthChanged
    {
        add => _healthChanged.AddListener(value);
        remove => _healthChanged.RemoveListener(value);
    }

    public void TakeDamage(float damage)
    {
        if (_health > 0)
        {
            _health -= damage;
        }
        _healthChanged.Invoke();
    }

    public void TakeHeal(float health)
    {
        if (_health < _maxHealth)
        {
            _health += health;

            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
        }
        _healthChanged.Invoke();
    }
}
