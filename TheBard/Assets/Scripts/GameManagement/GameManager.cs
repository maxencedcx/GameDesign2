using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int levelId = 1;
    [SerializeField] GameObject parent;
    private Controls _controls;
    public InGameObjects InGameObjects;
    public Dictionary<int, Vector3> slots = new Dictionary<int, Vector3>
    {
        { 1, new Vector3(2, -1.5f, 0)},
        { 2, new Vector3(2, 1.5f, 0)},
        { 3, new Vector3(4.5f, -3, 0)},
        { 4, new Vector3(4.5f, 3, 0)}
    };

    private void Awake()
    {
        base.Awake();

        _controls = new Controls();
    }

    private void Start()
    {
        LoadGame();
    }

    private void OnEnable()
    {
        _controls.InGameBard.Pause.performed += PauseGame;
        _controls.InGameBard.Pause.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameBard.Pause.performed -= PauseGame;
        _controls.InGameBard.Pause.Disable();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        Time.timeScale = 0;
    }

    private void LoadGame()
    {
        InGameObjects = new InGameObjects();
        GameSettings.Instance.LoadSettingsForLevel(levelId);

        GameObject player = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.playerPrefab), parent.transform);
        InGameObjects.setPlayer(player);

        foreach (EntitiesSettings es in GameSettings.Instance.entitiesSettings)
        {
            GameObject en = Instantiate(ResourcesManager.Instance.Get(es.PrefabType + Constants.Resources.suffixPrefab), parent.transform);
            en.GetComponent<IEntity>().Init(es.Type, es.Health, es.Damages, es.AttackSpeed, es.Immunities);
            en.transform.position = slots[en.GetComponent<IEntity>().getId()];
            if (es.Type == EntityType.ALLY)
            {
                Vector3 tmpPos = en.transform.position;
                tmpPos.x *= -1;
                en.transform.position = tmpPos;
                en.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}

public class InGameObjects
{
    private GameObject Player;
    private Dictionary<int, GameObject> Allies;
    private Dictionary<int, GameObject> Enemies;

    public InGameObjects()
    {
        Allies = new Dictionary<int, GameObject>();
        Enemies = new Dictionary<int, GameObject>();
    }

    public void setPlayer(GameObject player)
    {
        Player = player;
    }

    public int AddEntity(GameObject entity, EntityType type)
    {
        int id = 0;
        if (type == EntityType.ALLY)
        {
            id = Allies.Count + 1;
            Allies.Add(id, entity);
        }
        else if (type == EntityType.ENNEMY)
        {
            id = Enemies.Count + 1;
            Enemies.Add(id, entity);
        }
        return id;
    }
    public void RemoveEntity(int id, EntityType type)
    {
        if (type == EntityType.ALLY)
            Allies.Remove(id);
        else if (type == EntityType.ENNEMY)
            Enemies.Remove(id);
    }

    public Dictionary<int, IEntity> getAllAllies()
    {
        Dictionary<int, IEntity> allAllies = new Dictionary<int, IEntity>();
        foreach (KeyValuePair<int, GameObject> entry in Allies)
            allAllies.Add(entry.Key, entry.Value.GetComponent<IEntity>());
        return allAllies;
    }

    public Dictionary<int, IEntity> getAllEnemies()
    {
        Dictionary<int, IEntity> allEnemies = new Dictionary<int, IEntity>();
        foreach (KeyValuePair<int, GameObject> entry in Enemies)
            allEnemies.Add(entry.Key, entry.Value.GetComponent<IEntity>());
        return allEnemies;
    }

    public IEntity getClosestEnemy(float yPos, EntityType type)
    {
        if (type == EntityType.ALLY)
        {
            if (Enemies.Count == 0)
                return null;
            else
            {
                var entry = Enemies.Where(x => (yPos > 0) ? (x.Value.transform.position.y > 0) : (x.Value.transform.position.y < 0)).OrderBy(x => x.Key).FirstOrDefault();
                if (entry.Value == null)
                    entry = Enemies.First();
                return (entry.Value == null) ? (null) : (entry.Value.GetComponent<IEntity>());
            }
        }
        else if (type == EntityType.ENNEMY)
        {
            if (Allies.Count == 0)
                return null;
            else
            {
                var entry = Allies.Where(x => (yPos > 0) ? (x.Value.transform.position.y > 0) : (x.Value.transform.position.y < 0)).OrderBy(x => x.Key).FirstOrDefault();
                if (entry.Value == null)
                    entry = Allies.First();
                return (entry.Value == null) ? (null) : (entry.Value.GetComponent<IEntity>());
            }
        }

        return null;
    }
}