﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 100;
    public int Id { get; protected set; }
    protected int _health;
    virtual protected int Health
    {
        get { return _health; }
        set
        {
            if (_health != 0 && value != _health)
            {
                _health = Mathf.Clamp(value, 0, maxHealth);
                OnHealthChange(value);
                if (_health == 0)
                    Die();
            }
        }
    }

    void Awake()
    {
        _health = maxHealth;
    }


    public virtual void TakeDamage(int damage)
    {
        Debug.Log("TakeDamage");
        if (damage > 0)
            Health -= damage;
        Debug.Log(Health);
    }

    protected virtual void OnHealthChange(int value)
    { /*Update lifebar, activate red or green blink, play sound...*/}

    protected virtual void Die()
    {
        Debug.Log(Id + " -- DEAD");
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        /*if (GameManager.Instance != null && GameManager.Instance.activeRound != null && !GameManager.Instance.activeRound.isGameOver())
        {
            Health = 0;
            GameManager.Instance.activeRound.removePlayer(Id);
            if (Health <= 0)
                AudioManager.Instance.PlaySound("regularDeath");
            else
                AudioManager.Instance.PlaySound("fallingDeath");
        }*/
    }
}
