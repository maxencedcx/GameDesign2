using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Controls _controls;
    private Queue<string> _notes;
    private Dictionary<string, System.Action> _spells;
    private Dictionary<string, string> _keyToNote;

    private void Awake()
    {
        _controls = new Controls();
        _notes = new Queue<string>();
        _spells = new Dictionary<string, System.Action>();
        _spells.Add("DoMiSol", SpellTest);
        _spells.Add("ReFaLa", SpellTest);
        _spells.Add("MiSolSi", SpellTest);
        _spells.Add("FaLaDo", SpellTest);
        _spells.Add("SolSiRe", SpellTest);
        _spells.Add("LaDoMi", SpellTest);
        _spells.Add("SiReFa", SpellTest);
        _spells.Add("MiSiRe", SpellTest);
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

    private void OnEnable()
    {
        _controls.InGameBard.PressKey.performed += HandleKeyPressed;
        _controls.InGameBard.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameBard.PressKey.performed -= HandleKeyPressed;
        _controls.InGameBard.Disable();
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

    private void SpellTest()
    {
        //play good chords sound
        foreach (IEntity entity in GameManager.Instance.InGameObjects.getAllEnemies().Values)
            entity.debuffAttackSpeed(2, 10);
        Debug.Log("Spell called!");
    }

    private void SpellIncorrect()
    {
        //play bad chords sound
        AudioManager.Instance.PlaySound("FailureJingle");
        Debug.Log("INCORRECT SPELL");
    }
}
