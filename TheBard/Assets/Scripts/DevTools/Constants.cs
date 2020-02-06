public static class Constants
{
    #region ScenesNames
    public static class ScenesNames
    {
        public const string game = "Game";
    }
    #endregion

    #region Tags
    public static class Tags
    {
    }
    #endregion

    #region Resources
    public static class Resources
    {
        // PREFABS
        public const string prefabsPath = "Prefabs/";
        public const string suffixPrefab = "_prefab";
        public const string playerPrefab = "PLAYER" + suffixPrefab;
        public const string entityPrefab = "ENTITY" + suffixPrefab;
        public const string cowboyPrefab = "COWBOY" + suffixPrefab;
        public const string knightPrefab = "KNIGHT" + suffixPrefab;

        // SOUNDS
        public const string soundsPath = "Sounds/";
        public const string effectsPath = soundsPath + "Effects/";
        public const string musicsPath = soundsPath + "Musics/";
    }
    #endregion

    #region Exceptions
    public static class Exceptions
    {
        public static class MonoSingleton
        {
            public const string instanceAlreadyExists = "There is already an singleton instance of {0}";
            public const string instanceNotFound = "There is no singleton instance of {0}";
        }

        public static class Switch
        {
            public const string instanceNotFound = "There is no switch case for {0}";
        }
    }
    #endregion
}
