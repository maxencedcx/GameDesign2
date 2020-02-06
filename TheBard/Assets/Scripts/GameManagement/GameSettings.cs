using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoSingleton<GameSettings>
{
    public List<EntitiesSettings> entitiesSettings;
    protected override void Awake()
    {
        base.Awake();

        entitiesSettings = new List<EntitiesSettings>();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSettingsForLevel(int levelId)
    {
        //load the file for this levelId
        entitiesSettings.Add(new EntitiesSettings(EntityType.ENNEMY, 100, 10, 1));
        entitiesSettings.Add(new EntitiesSettings(EntityType.ALLY, 100, 10, 1));
    }
}


public struct EntitiesSettings
{
    //EntityType type = (EntityType)System.Enum.Parse(typeof(EntityType), "ENEMY");
    public EntityType Type;
    public int Health;
    public int Damages;
    public int AttackSpeed;

    public EntitiesSettings(EntityType type, int health, int damages, int attackspeed)
    {
        Type = type;
        Health = health;
        Damages = damages;
        AttackSpeed = attackspeed;
    }
}