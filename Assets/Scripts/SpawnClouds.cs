using UnityEngine;

public class SpawnClouds : MonoBehaviour {
    public Level level;
    public GameObject ground;
    public GameObject cloud;
    public float minimumCloudHeight = 1;
    public float maximumCloudHeight = 1;
    private float distance;
    private float spawnAt;

    private void Start() {
        distance = 0f;
        spawnAt = 0f;
    }

    private void Update() {
        distance += Time.deltaTime * level.mainSpeed;
        if (distance < spawnAt) return;
        SpawnCloud();
    }

    private void SpawnCloud() {
        var obstacle = Instantiate(cloud);
        obstacle.transform.position = new Vector3(transform.localScale.x / 2,
            minimumCloudHeight + Random.value * (maximumCloudHeight - minimumCloudHeight), 0);
        obstacle.GetComponent<MoveRelatively>().level = level;
        obstacle.GetComponent<DestroyOnLeftEdge>().ground = gameObject;

        spawnAt = (3 + 100 + Random.value * 300) * 4;
        distance = 0f;
    }
}