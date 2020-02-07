﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Queue<string> _notes;
    private Dictionary<string, System.Action> _spells;
    private Dictionary<string, string> _keyToNote;

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
        //add chords and corresponding spells

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

    private void Start()
    {
        GameManager.Instance.addActionToInputAction("PressKey", HandleKeyPressed);
        GameManager.Instance.enableInputActionByName("PressKey");
    }

    private void OnDestroy()
    {
        GameManager.Instance.removeActionToInputAction("PressKey", HandleKeyPressed);
        GameManager.Instance.disableInputActionByName("PressKey");
    }

    private void HandleKeyPressed(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlaySound(context.control.displayName);
        _notes.Enqueue(_keyToNote[context.control.displayName]);
        if (_notes.Count == 3)
        {
            string chords = string.Concat(_notes);
            Debug.Log("Chords: " + chords);
            _notes.Clear();
            if (_spells.ContainsKey(chords))
            {
                AudioManager.Instance.PlaySound(chords + "Chords");
                _spells[chords]();
            }
            else
                SpellIncorrect();
        }
    }

    private void DebuffStunSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllEnemies().Values)
            entity.debuffStun(2);
        Debug.Log("DebuffStunSpell called!");
    }

    private void DebuffAttackspeedSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllEnemies().Values)
            entity.debuffAttackSpeed(0.2, 5);
        Debug.Log("DebuffAttackspeedSpell called!");
    }

    private void DebuffAttackDamagesSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllEnemies().Values)
            entity.debuffAttackDamages(1, 5);
        Debug.Log("DebuffAttackDamagesSpell called!");
    }

    private void BuffHealSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllAllies().Values)
            entity.HealDamage(10);
        Debug.Log("BuffHealSpell called!");
    }

    private void BuffAttackspeedSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllAllies().Values)
            entity.buffAttackSpeed(0.2, 5);
        Debug.Log("BuffAttackspeedSpell called!");
    }

    private void BuffAttackDamagesSpell()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllAllies().Values)
            entity.buffAttackDamages(1, 5);
        Debug.Log("BuffAttackDamagesSpell called!");
    }

    private void SpellTest()
    {
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllEnemies().Values)
            entity.TakeDamage(10);
        Debug.Log("Spell called!");
    }

    private void SpellIncorrect()
    {
        AudioManager.Instance.PlaySound("FailureJingle");
        Debug.Log("INCORRECT SPELL");
    }
}
