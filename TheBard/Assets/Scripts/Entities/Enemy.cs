using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public void Init(int Id)
    {
        this.Id = Id;
        GameManager.Instance.InGameObjects.AddEnemy(Id, gameObject);
    }
}
