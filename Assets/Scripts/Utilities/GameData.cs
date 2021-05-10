using System.Collections.Generic;

// Class to store all game data
[System.Serializable]
public class GameData {
    // Values to hold PlayerUpgrade values
    public static int baseCost;
    public static Dictionary<Upgradeable, float> playerUpgrades = new Dictionary<Upgradeable, float>();
    // Asteroid-focused value(s)
    public static double damageFactor;
    public static int asteroidSpeed;
    public static int explosiveForce;
    public static int bufferZone;
}
