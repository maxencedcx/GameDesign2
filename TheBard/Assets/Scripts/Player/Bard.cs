using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bard : MonoBehaviour
{
    [SerializeField] protected EntityType type = EntityType.DEFAULT;
    protected Queue<string> _notes;
    protected Dictionary<string, System.Action> _spells;
    protected Dictionary<string, string> _keyToNote;

    private void Awake()
    {
        _notes = new Queue<string>();
        _spells = new Dictionary<string, System.Action>();
        _spells.Add("DoMiSol", BuffHealSpell);
        _spells.Add("ReFaLa", DebuffStunSpell);
        _spells.Add("MiSolSi", DebuffAttackspeedSpell);
        _spells.Add("FaLaDo", BuffAttackDamagesSpell);
        _spells.Add("SolSiRe", SpellTest);
        _spells.Add("LaDoMi", SpellTest);
        _spells.Add("SiReFa", DebuffAttackDamagesSpell);
        _spells.Add("MiSiRe", BuffAttackDamagesSpell);

        _keyToNote = new Dictionary<string, string>
        {
            { "A", "Do" },
            { "Z", "Re" },
            { "E", "Mi" },
            { "R", "Fa" },
            { "Q", "Sol" },
            { "S", "La" },
            { "D", "Si" },
            { "F", "Do" }
        };
    }

    protected void DebuffStunSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllContraryEntityTypes(type).Values)
            entity.debuffStun(2);
    }

    protected void DebuffAttackspeedSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllContraryEntityTypes(type).Values)
            entity.debuffAttackSpeed(0.2, 5);
    }

    protected void DebuffAttackDamagesSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllContraryEntityTypes(type).Values)
            entity.debuffAttackDamages(1, 5);
    }

    protected void BuffHealSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllSimilarEntityTypes(type).Values)
            entity.HealDamage(10);
    }

    protected void BuffAttackspeedSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllSimilarEntityTypes(type).Values)
            entity.buffAttackSpeed(0.2, 5);
    }

    protected void BuffAttackDamagesSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllSimilarEntityTypes(type).Values)
            entity.buffAttackDamages(1, 5);
    }

    protected void SpellTest()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllContraryEntityTypes(type).Values)
            entity.TakeDamage(10);
    }

    protected void SpellIncorrect()
    {
        AudioManager.Instance.PlaySound("FailureJingle");
    }
}
