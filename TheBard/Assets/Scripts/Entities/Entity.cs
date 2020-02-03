using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    public int Id { get; private set; }
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

    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    { }

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
            Health -= damage;
    }

    protected virtual void OnHealthChange(int value)
    { /*Update lifebar, activate red or green blink, play sound...*/}

    protected virtual void Die()
    {
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
