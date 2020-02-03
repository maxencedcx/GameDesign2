using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoSingleton<GameSettings>
{

    /// <summary>
    /// Creates an instance of GameSettings if it doesn't exist.
    /// If it does, keeps it.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }
}
