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
    void Init(int Id, EntityType type);
    void TakeDamage(int damage);
    void HealDamage(int heal);
    void debuffStun(bool stunned, int duration);
    void debuffAttackSpeed(double value, int duration);
    void debuffAttackDamages(int value, int duration);
    void buffAttackSpeed(double value, int duration);
    void buffAttackDamages(int value, int duration);
}
