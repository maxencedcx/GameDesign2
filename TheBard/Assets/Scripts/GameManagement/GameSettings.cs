using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSettings : MonoSingleton<GameSettings>
{
    public List<EntitiesSettings> entitiesSettings;
    protected override void Awake()
    {
        base.Awake();

        entitiesSettings = new List<EntitiesSettings>();
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        entitiesSettings.Clear();
    }

    public void LoadSettingsForLevel(int levelId)
    {
        string line;
        string path = "Assets/Resources/Levels/LEVEL_"+levelId+".txt";

        StreamReader file = new StreamReader(path);

        while ((line = file.ReadLine()) != null)
        {
            string[] entitySetting = line.Split('|');
            List<Immunity> immunities = new List<Immunity>();
            if (entitySetting.Length > 5)
                for (int i = 5; i < entitySetting.Length; i++)
                    immunities.Add((Immunity)System.Enum.Parse(typeof(Immunity), entitySetting[i]));

            entitiesSettings.Add(new EntitiesSettings(entitySetting[0], (EntityType)System.Enum.Parse(typeof(EntityType), entitySetting[1]), int.Parse(entitySetting[2]), int.Parse(entitySetting[3]), double.Parse(entitySetting[4]), immunities));
        }

        file.Close();
    }
}


public struct EntitiesSettings
{
    public string PrefabType;
    public EntityType Type;
    public int Health;
    public int Damages;
    public double AttackSpeed;
    public List<Immunity> Immunities;

    public EntitiesSettings(string prefabtype, EntityType type, int health, int damages, double attackspeed, List<Immunity> immunities)
    {
        PrefabType = prefabtype;
        Type = type;
        Health = health;
        Damages = damages;
        AttackSpeed = attackspeed;
        Immunities = immunities;
    }
}