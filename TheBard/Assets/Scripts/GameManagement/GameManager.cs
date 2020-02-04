using System.Collections;
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

        GameObject enemy = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.enemyPrefab));
        enemy.GetComponent<Enemy>().Init(1);
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

    public void AddAlly(int id, GameObject entity)
    {
        Allies.Add(id, entity);
    }

    public void AddEnemy(int id, GameObject entity)
    {
        Enemies.Add(id, entity);
    }

    public Dictionary<int, GameObject> getAllAllies()
    {
        return Allies;
    }

    public Dictionary<int, GameObject> getAllEnemies()
    {
        return Enemies;
    }
}