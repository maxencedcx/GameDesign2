﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected EntityType Type = EntityType.DEFAULT;
    [SerializeField] protected double attackSpeed = 1;
    [SerializeField] protected int attackDamages = 10;
    [SerializeField] protected bool isStunned = false;
    private float lastAttack = 0;
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

    private void Update()
    {
        if (Time.time - lastAttack >= attackSpeed && isStunned == false)
        {
            lastAttack = Time.time;
            GetComponent<Animator>().Play("Knight_Attack");
        }
    }

    public void debuffStun(bool stunned, int duration)
    {
        //stun
        OnDebuff();
        StartCoroutine(coroutineDebuffStun(stunned, duration));
    }

    IEnumerator coroutineDebuffStun(bool stunned, int duration)
    {
        isStunned = true;
        yield return new WaitForSeconds(duration);
        isStunned = false;
    }

    public void debuffAttackDamages(int value, int duration)
    {
        //change attack damages
        OnDebuff();
        StartCoroutine(coroutineDebuffAttackDamages(value, duration));
    }

    IEnumerator coroutineDebuffAttackDamages(int value, int duration)
    {
        attackDamages -= value;
        yield return new WaitForSeconds(duration);
        attackDamages += value;
    }

    public void buffAttackDamages(int value, int duration)
    {
        //change attack damages
        OnBuff();
        StartCoroutine(coroutineBuffAttackDamages(value, duration));
    }

    IEnumerator coroutineBuffAttackDamages(int value, int duration)
    {
        attackDamages += value;
        yield return new WaitForSeconds(duration);
        attackDamages -= value;
    }

    public void debuffAttackSpeed(double value, int duration)
    {
        //change attack speed
        OnDebuff();
        StartCoroutine(coroutineDebuffAttackSpeed(value, duration));
    }

    IEnumerator coroutineDebuffAttackSpeed(double value, int duration)
    {
        attackSpeed += value;
        yield return new WaitForSeconds(duration);
        attackSpeed -= value;
    }

    public void buffAttackSpeed(double value, int duration)
    {
        //change attack speed
        OnBuff();
        StartCoroutine(coroutineBuffAttackSpeed(value, duration));
    }

    IEnumerator coroutineBuffAttackSpeed(double value, int duration)
    {
        attackSpeed -= value;
        yield return new WaitForSeconds(duration);
        attackSpeed += value;
    }

    public void Init(int Id, EntityType type)
    {
        this.Id = Id;
        this.Type = type;
        GameManager.Instance.InGameObjects.AddEntity(Id, gameObject, Type);
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
            Health -= damage;
        //GetComponent<Animator>().Play("Knight_Attack");
    }

    public virtual void HealDamage(int heal)
    {
        if (heal > 0)
            Health += heal;
        //GetComponent<Animator>().Play("Knight_Attack");
    }

    protected virtual void OnHealthChange(int value)
    {
        /*Update lifebar, activate red or green blink, play sound...*/
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), GetComponent<SpriteRenderer>().color, Color.red, 0.5f, 0.1f));
    }

    protected virtual void OnDebuff()
    {
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), GetComponent<SpriteRenderer>().color, Color.yellow, 0.5f, 0.1f));
    }

    protected virtual void OnBuff()
    {
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), GetComponent<SpriteRenderer>().color, Color.blue, 0.5f, 0.1f));
    }

    protected virtual void Die()
    {
        Debug.Log(Type.ToString() + Id + " -- DEAD");
        Destroy(gameObject);
    }

    IEnumerator FlashObject(SpriteRenderer toFlash, Color originalColor, Color flashColor, float flashTime, float flashSpeed)
    {
        float flashingFor = 0;
        var newColor = flashColor;
        while (flashingFor < flashTime)
        {
            toFlash.color = newColor;
            flashingFor += Time.deltaTime;
            yield return new WaitForSeconds(flashSpeed);
            flashingFor += flashSpeed;
            if (newColor == flashColor)
                newColor = originalColor;
            else
                newColor = flashColor;
        }
        toFlash.color = originalColor;
    }


    private void OnDestroy()
    {
        GameManager.Instance.InGameObjects.RemoveEntity(Id, Type);
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
