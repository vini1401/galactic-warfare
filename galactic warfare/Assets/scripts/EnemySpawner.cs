using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnInterval = 1.5f;   // tempo entre spawns
    public float spawnDuration = 7f;     // tempo total spawnando

    private float spawnTimer;
    private float durationTimer;
    private bool canSpawn = true;

    void Update()
    {
        if (!canSpawn)
            return;

        durationTimer += Time.deltaTime;

        if (durationTimer >= spawnDuration)
        {
            canSpawn = false;
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Camera cam = Camera.main;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        float spawnX = cam.transform.position.x + camWidth + 1f;
        float spawnY = Random.Range(
            cam.transform.position.y - camHeight,
            cam.transform.position.y + camHeight
        );

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
