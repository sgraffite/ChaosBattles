using UnityEngine;

public class World : MonoBehaviour {
    private PlayerController playerControllerInstance;
    private EnemySpawner enemySpawnerInstance;

	void Start () {
        playerControllerInstance = FindObjectOfType<PlayerController>();

        // Spawn enemies
        enemySpawnerInstance = FindObjectOfType<EnemySpawner>();
        enemySpawnerInstance.SpawnEnemyAtEnemyPositions();
    }

    void Update () {
        // TODO: move player
        // TODO: move enemies
    }
}
