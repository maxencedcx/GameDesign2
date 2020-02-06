using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    DEFAULT,
    ALLY,
    ENNEMY
}

public interface IEntity
{
    void Init(EntityType type, int health, int damages, int attackspeed);
    void TakeDamage(int damage);
    void HealDamage(int heal);
    void debuffStun(int duration);
    void debuffAttackSpeed(double value, int duration);
    void debuffAttackDamages(int value, int duration);
    void buffAttackSpeed(double value, int duration);
    void buffAttackDamages(int value, int duration);
}
