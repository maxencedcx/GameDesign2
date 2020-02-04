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
}
