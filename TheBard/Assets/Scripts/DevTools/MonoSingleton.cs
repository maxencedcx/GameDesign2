using System;
using UnityEngine;

[DisallowMultipleComponent]
public class MonoSingleton<TClass> : MonoBehaviour where TClass : MonoBehaviour
{
    #region Instance Handling
    private static MonoSingleton<TClass> _instance = null;

    protected virtual void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            throw new Exception(Constants.Exceptions.MonoSingleton.instanceAlreadyExists.FormatWith(GetType().Name));
    }

    public static TClass Instance
    {
        get
        {
            if (_instance != null)
                return _instance as TClass;
            else
                throw new Exception(Constants.Exceptions.MonoSingleton.instanceNotFound.FormatWith(typeof(TClass).Name));
        }
    }

    public static bool HasInstance
    {
        get { return _instance != null; }
    }
    #endregion
}
