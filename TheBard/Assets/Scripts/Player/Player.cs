using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Player : Bard
{
    [SerializeField] private GameObject displayNotes;

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
        string chords = string.Concat(_notes);
        displayNotes.GetComponent<TMP_Text>().color = Color.white;
        displayNotes.GetComponent<TMP_Text>().text = chords;
        if (_notes.Count == 3)
        {
            _notes.Clear();
            if (_spells.ContainsKey(chords))
            {
                displayNotes.GetComponent<TMP_Text>().color = Color.green;
                AudioManager.Instance.PlaySound(chords + "Chords");
                _spells[chords]();
            }
            else
            {
                displayNotes.GetComponent<TMP_Text>().color = Color.red;
                SpellIncorrect();
            }
        }
    }
}
