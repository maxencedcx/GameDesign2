﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    DEFAULT,
    ALLY,
    ENNEMY
}

public enum Immunity
{
    STUN,
    DEBUFFAD,
    DEBUFFAS,
}



public interface IEntity
{
    void Init(EntityType type, int health, int damages, double attackspeed, List<Immunity> immunities = null);
    void TakeDamage(int damage);
    void HealDamage(int heal);
    void debuffStun(int duration);
    void debuffAttackSpeed(double value, int duration);
    void debuffAttackDamages(int value, int duration);
    void buffAttackSpeed(double value, int duration);
    void buffAttackDamages(int value, int duration);
    int getId();
    bool getIsDead();
}
