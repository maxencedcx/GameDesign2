using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoSingleton<ResourcesManager>
{
    [SerializeField] private GameObject playerPrefab;

    private Dictionary<string, GameObject> objects;

    /// <summary>
    /// Creates an instance of ResourcesManager if it doesn't exist.
    /// If it does, keeps it.
    /// Store the default GameObjects.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();

        objects = new Dictionary<string, GameObject>
        {
            { Constants.Resources.playerPrefab, playerPrefab },
        };
    }

    /// <summary>
    /// Stores the specified GameObject with the specified name.
    /// </summary>
    /// <param name="name">The name of the specified object.</param>
    /// <param name="obj">The game object.</param>
    public void Add(string name, GameObject obj) => objects.Add(name, obj);


    /// <summary>
    /// Returns the GameObject corresponding to the specified name.
    /// If it is not found in the Dictionary, it will try to load a GameObject with the same name.
    /// </summary>
    /// <param name="name">the name of the GameObject you're looking for.</param>
    /// <returns>Returns the game object, or null if it could not be loaded.</returns>
    public GameObject Get(string name)
    {
        if (objects.ContainsKey(name))
            return objects[name];
        else
        {
            GameObject tmp = Resources.Load<GameObject>(name);
            if (tmp != null)
            {
                Add(name, tmp);
                Debug.Log("[RESOURCES_MANAGER]: Loaded '" + name + "'.");
                return objects[name];
            }
            else
            {
                Debug.Log("[RESOURCES_MANAGER]: Could not load '" + name + "'.");
                return null;
            }
        }
    }
}
