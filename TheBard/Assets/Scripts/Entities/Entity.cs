using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour, IEntity
{
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected EntityType Type = EntityType.DEFAULT;
    [SerializeField] protected double attackSpeed = 1;
    [SerializeField] protected int attackDamages = 10;
    [SerializeField] protected float stunnedFor = 0;
    private float lastAttack = 0;
    public int Id { get; protected set; }
    protected int _health;
    public Slider HealthBar;

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

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (stunnedFor > 0)
        {
            stunnedFor = Mathf.Clamp(stunnedFor - Time.deltaTime, 0, stunnedFor);
        }
        else if (Time.time - lastAttack >= attackSpeed)
        {
            lastAttack = Time.time;
            GetComponent<Animator>().Play("Knight_Attack");
        }
    }

    public void debuffStun(int duration)
    {
        //stun
        OnDebuff();
        stunnedFor += duration;
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

    public void Init(EntityType type, int health, int damages, int attackspeed)
    {
        this.Type = type;
        this.maxHealth = health;
        this.attackDamages = damages;
        this.attackSpeed = attackspeed;
        this.Id = GameManager.Instance.InGameObjects.AddEntity(gameObject, Type);
        _health = maxHealth - 50;
        gameObject.SetActive(true);
    }

    public virtual void TakeDamage(int damage)
    {
        if (damage > 0)
            Health -= damage;
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), Color.red, 0.5f, 0.1f));
        //GetComponent<Animator>().Play("Knight_Attack");
    }

    public virtual void HealDamage(int heal)
    {
        if (heal > 0)
            Health += heal;
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), Color.green, 0.5f, 0.1f));
        //GetComponent<Animator>().Play("Knight_Attack");
    }

    protected virtual void OnHealthChange(int value)
    {
        /*Update lifebar, activate red or green blink, play sound...*/
        HealthBar.value = _health;
    }

    protected virtual void OnDebuff()
    {
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), Color.yellow, 0.5f, 0.1f));
    }

    protected virtual void OnBuff()
    {
        StartCoroutine(FlashObject(GetComponent<SpriteRenderer>(), Color.blue, 0.5f, 0.1f));
    }

    protected virtual void Die()
    {
        Debug.Log(Type.ToString() + Id + " -- DEAD");
        Destroy(gameObject);
    }

    IEnumerator FlashObject(SpriteRenderer toFlash, Color flashColor, float flashTime, float flashSpeed)
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
                newColor = Color.white;
            else
                newColor = flashColor;
        }
        toFlash.color = Color.white;
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
