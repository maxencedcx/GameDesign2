using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBard : Bard
{
    private float attackSpeed = 2;
    private float lastAttack = 0;

    private void Start()
    {
        lastAttack = Time.time;
    }

    void Update()
    {
        if (Time.time - lastAttack >= attackSpeed)
        {
            lastAttack = Time.time;
            Enumerable.ToList(_spells.Values)[Random.Range(0, _spells.Count)]();
            if (attackSpeed > 0.5f)
                attackSpeed -= 0.1f;
        }
    }
}
