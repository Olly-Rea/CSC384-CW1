using System.Collections.Generic;

// Class to store all game data
[System.Serializable]
public class SaveData {

    // Game values
    public Difficulty difficultyLevel;
    public float scoreModifier;

    // Values to hold PlayerUpgrade values
    public int baseCost;
    public Dictionary<Upgradeable, float> playerUpgrades = new Dictionary<Upgradeable, float>();

    // Asteroid-focused values
    public double damageFactor;
    public int asteroidSpeed, explosiveForce, bufferZone;

    // Score value
    public int score;

    // UpgradeController values
    public int[] costs, levels;
    public float[] modifiers;

    // Player health values
    public float playerShield, playerHealth;

    // Construct the SaveData with reference to itself as "Instance"
    public SaveData(Difficulty _difficulty, int _baseCost, Dictionary<Upgradeable, float> _playerUpgrades, 
        double _damageFactor, int _asteroidSpeed, int _explosiveForce, int _bufferZone, float _scoreModifier, 
        int[] _costs, int[] _levels, float[] _modifiers, float _playerShield, float _playerHealth) {
        // Initialise all the values as serializable data
        difficultyLevel = _difficulty;
        baseCost = _baseCost;
        playerUpgrades = _playerUpgrades;
        // Asteroid-focused values
        damageFactor = _damageFactor;
        asteroidSpeed = _asteroidSpeed;
        explosiveForce = _explosiveForce;
        bufferZone = _bufferZone;
        // Game values
        scoreModifier = _scoreModifier;
        score = Score.Instance.GetScore();
        // UpgradeController stuff
        costs = _costs;
        levels = _levels;
        modifiers = _modifiers;
        // Player health stuff
        playerShield = _playerShield;
        playerHealth = _playerHealth;
    }

}
