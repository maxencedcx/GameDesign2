using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private int levelId = 1;
    [SerializeField] GameObject parent;
    [SerializeField] GameObject pauseImage;
    [SerializeField] GameObject defeatImage;
    [SerializeField] GameObject victoryImage;
    [SerializeField] GameObject blackPanel;
    [SerializeField] float loadingTime = 3.5f;
    private Controls _controls;
    private bool paused = false;
    private bool loadingGame = false;
    public InGameObjects InGameObjects;
    public Dictionary<int, Vector3> slots = new Dictionary<int, Vector3>
    {
        { 1, new Vector3(2, -2.5f, 0)},
        { 2, new Vector3(2, -0.5f, 0)},
        { 3, new Vector3(4.5f, -3, 0)},
        { 4, new Vector3(4.5f, 0, 0)}
    };

    private void Awake() 
    {
        base.Awake();

        _controls = new Controls();
    }

    private void Start()
    {
        InGameObjects = new InGameObjects();
        StartCoroutine(loadLevel(levelId));
    }

    private void Update()
    {
        if (loadingGame)
            return;
        if (InGameObjects.getAlliesCount() == 0)
            StartCoroutine(goBackToMenu());
        else if (InGameObjects.getEnemiesCount() == 0)
            StartCoroutine(loadLevel(++levelId));
    }

    public void addActionToInputAction(string inputActionName, System.Action<InputAction.CallbackContext> action)
    {
        var inputAction = _controls.InGameBard.Get().actions.Where(x => x.name == inputActionName).FirstOrDefault();
        if (inputAction == null)
            return;
        inputAction.performed += action;
    }
    public void removeActionToInputAction(string inputActionName, System.Action<InputAction.CallbackContext> action)
    {
        var inputAction = _controls.InGameBard.Get().actions.Where(x => x.name == inputActionName).FirstOrDefault();
        if (inputAction == null)
            return;
        inputAction.performed -= action;
    }

    public void enableInputActionByName(string inputActionName)
    {
        var inputAction = _controls.InGameBard.Get().actions.Where(x => x.name == inputActionName).FirstOrDefault();
        if (inputAction == null)
            return;
        inputAction.Enable();
    }

    public void disableInputActionByName(string inputActionName)
    {
        var inputAction = _controls.InGameBard.Get().actions.Where(x => x.name == inputActionName).FirstOrDefault();
        if (inputAction == null)
            return;
        inputAction.Disable();
    }

    private void OnEnable()
    {
        _controls.InGameBard.Pause.performed += PauseGame;
        _controls.InGameBard.Enable();
    }

    private void OnDisable()
    {
        _controls.InGameBard.Pause.performed -= PauseGame;
        _controls.InGameBard.Disable();
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        if (paused == false)
        {
            Time.timeScale = 0;
            _controls.InGameBard.PressKey.Disable();
            paused = true;
        }
        else if (paused == true)
        {
            Time.timeScale = 1;
            _controls.InGameBard.PressKey.Enable();
            paused = false;
        }
        blackPanel.SetActive(paused);
        pauseImage.SetActive(paused);
    }

    private void LoadGame()
    {
        GameSettings.Instance.LoadSettingsForLevel(levelId);

        GameObject player = Instantiate(ResourcesManager.Instance.Get(Constants.Resources.playerPrefab), parent.transform);
        InGameObjects.setPlayer(player);

        if (levelId == 5)
            Instantiate(ResourcesManager.Instance.Get(Constants.Resources.enemyBardPrefab), parent.transform);

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

    IEnumerator loadLevel(int levelId)
    {
        loadingGame = true;
        InGameObjects.Reset();
        GameSettings.Instance.Reset();
        if (levelId > 1)
        {
            AudioManager.Instance.PlaySound("VictoryJingle");
            blackPanel.SetActive(true);
            victoryImage.SetActive(true);
            yield return new WaitForSeconds(loadingTime);
            blackPanel.SetActive(false);
            victoryImage.SetActive(false);
        }
        loadingGame = false;
        if (levelId > 5)
            SceneManager.LoadScene("Credits");
        else
            LoadGame();
    }

    IEnumerator goBackToMenu()
    {
        AudioManager.Instance.PlaySound("DefeatJingle");
        loadingGame = true;
        InGameObjects.Reset();
        GameSettings.Instance.Reset();
        blackPanel.SetActive(true);
        defeatImage.SetActive(true);
        yield return new WaitForSeconds(loadingTime);
        blackPanel.SetActive(false);
        defeatImage.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}

public class InGameObjects
{
    private GameObject Player;
    private Dictionary<int, GameObject> Allies;
    private Dictionary<int, GameObject> Enemies;
    public List<GameObject> DeadEntities;

    public InGameObjects()
    {
        Allies = new Dictionary<int, GameObject>();
        Enemies = new Dictionary<int, GameObject>();
        DeadEntities = new List<GameObject>();
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

    public Dictionary<int, IEntity> getAllContraryEntityTypes(EntityType type)
    {
        return (type == EntityType.ALLY) ? (getAllEnemies()) : (getAllAllies());
    }

    public Dictionary<int, IEntity> getAllSimilarEntityTypes(EntityType type)
    {
        return (type == EntityType.ALLY) ? (getAllAllies()) : (getAllEnemies());
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
                var entry = Enemies.Where(x => ((yPos > -1) ? (x.Value.transform.position.y > -1) : (x.Value.transform.position.y < 0))).OrderBy(x => x.Key).FirstOrDefault();
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
                var entry = Allies.Where(x => ((yPos > -1) ? (x.Value.transform.position.y > -1) : (x.Value.transform.position.y < 0))).OrderBy(x => x.Key).FirstOrDefault();
                if (entry.Value == null)
                    entry = Allies.First();
                return (entry.Value == null) ? (null) : (entry.Value.GetComponent<IEntity>());
            }
        }

        return null;
    }

    public int getAlliesCount()
    { return Allies.Count; }

    public int getEnemiesCount()
    { return Enemies.Count; }


    public void Reset()
    {
        GameObject.Destroy(Player);
        foreach (var entry in Allies.Values)
            GameObject.Destroy(entry);
        foreach (var entry in Enemies.Values)
            GameObject.Destroy(entry);
        foreach (var entry in DeadEntities)
            GameObject.Destroy(entry);
        Allies.Clear();
        Enemies.Clear();
        DeadEntities.Clear();
    }
}