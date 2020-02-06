using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int levelId = 1;
    public InGameObjects InGameObjects;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        InGameObjects = new InGameObjects();
        GameSettings.Instance.LoadSettingsForLevel(levelId);

        GameObject player = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.playerPrefab));
        InGameObjects.setPlayer(player);

        foreach (EntitiesSettings es in GameSettings.Instance.entitiesSettings)
        {
            GameObject en = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.entityPrefab));
            en.GetComponent<IEntity>().Init(es.Type, es.Health, es.Damages, es.AttackSpeed);
            if (es.Type == EntityType.ALLY)
            {
                en.transform.position = new Vector3(-3, 0, 0);
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
            id = Allies.Count;
            Allies.Add(id, entity);
        }
        else if (type == EntityType.ENNEMY)
        {
            id = Enemies.Count;
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
}