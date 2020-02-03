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
        _spells.Add("DoReMi", SpellTest);

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
        _controls.InGameBard.PressSpace.performed += SpellLaunched;
        _controls.InGameBard.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameBard.PressKey.performed -= HandleKeyPressed;
        _controls.InGameBard.PressSpace.performed -= SpellLaunched;
        _controls.InGameBard.Disable();
    }

    private void HandleKeyPressed(InputAction.CallbackContext context)
    {
        //Debug.Log("keyPressed " + context.control.displayName);
        _notes.Enqueue(_keyToNote[context.control.displayName]);
        if (_notes.Count > 3)
            _notes.Dequeue();
    }

    private void SpellLaunched(InputAction.CallbackContext context)
    {
        string chords = string.Concat(_notes);
        Debug.Log("SpellLaunched: " + chords);
        _notes.Clear();
        if (_spells.ContainsKey(chords))
            _spells[chords]();
        else
            SpellIncorrect();
    }

    private void SpellTest()
    {
        Debug.Log("Spell called!");
    }

    private void SpellIncorrect()
    {
        Debug.Log("INCORRECT SPELL");
    }
}
