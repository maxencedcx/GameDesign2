﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public InGameObjects InGameObjects;

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        InGameObjects = new InGameObjects();

        GameObject player = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.playerPrefab));
        InGameObjects.setPlayer(player);

        GameObject enemy = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.entityPrefab));
        enemy.GetComponent<IEntity>().Init(1, EntityType.ENNEMY);

        GameObject ally = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.entityPrefab));
        ally.GetComponent<IEntity>().Init(1, EntityType.ALLY);
        ally.transform.position = new Vector3(-3, 0, 0);
        ally.GetComponent<SpriteRenderer>().flipX = false;
        Debug.Log(ally.transform);
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

    public void AddEntity(int id, GameObject entity, EntityType type)
    {
        if (type == EntityType.ALLY)
            Allies.Add(id, entity);
        else if (type == EntityType.ENNEMY)
            Enemies.Add(id, entity);
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