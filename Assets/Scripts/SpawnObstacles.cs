using UnityEngine;

public class SpawnObstacles : MonoBehaviour {
    public Level level;
    private readonly string[] obstacleHistory = { null, null };
    private float distance;
    private int obstacleHistoryIndex;
    private float spawnAt;

    private void Start() {
        distance = 0f;
        spawnAt = 0f;
    }

    private void Update() {
        distance += Time.deltaTime * level.mainSpeed;
        if (distance < spawnAt) return;
        SpawnObstacle();
    }

    private void SpawnObstacle() {
        int obstacleIndex;
        ObstacleStuff obstacleStuff = null;
        string uniqueName;
        do {
            obstacleIndex = Random.Range(0, level.obstacles.Length);
            obstacleStuff = level.obstacles[obstacleIndex].GetComponent<ObstacleStuff>();
        } while ((obstacleStuff.uniqueName == obstacleHistory[0] && obstacleStuff.uniqueName == obstacleHistory[1]) ||
                 obstacleStuff.minSpeed > level.mainSpeed);

        obstacleHistory[obstacleHistoryIndex] = obstacleStuff.uniqueName;
        obstacleHistoryIndex = (obstacleHistoryIndex + 1) % 2;

        var obstacle = Instantiate(level.obstacles[obstacleIndex]);
        obstacle.transform.position = new Vector3(transform.localScale.x / 2, transform.position.y, 0);
        obstacle.GetComponent<MoveRelatively>().level = level;
        obstacle.GetComponent<DestroyOnLeftEdge>().ground = gameObject;

        var minimumSpawnDistance = obstacle.GetComponent<Collider2D>().bounds.size.x * level.mainSpeed / 4.0f +
                                   obstacle.GetComponent<ObstacleStuff>().minGap * 0.6f;
        spawnAt = minimumSpawnDistance * (1 + Random.value * 0.5f);
        distance = 0f;
    }
}