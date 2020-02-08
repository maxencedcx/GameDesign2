using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Credits : MonoBehaviour
{
    private Controls controls;
    void Start()
    {
        controls = new Controls();

        controls.InMenu.AnyKey.performed += quitApplication;
        controls.InMenu.Enable();
    }

    public void quitApplication(InputAction.CallbackContext context)
    {
        Debug.Log("Quitting game");
        Application.Quit();
    }
}
