using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Level : MonoBehaviour {
    public GameObject[] obstacles;
    public float mainSpeed = 10;
    public float maxSpeed = 10;
    public float acceleration = 10;
    private float distanceRan;
    private bool addDistance = true;
    public int localInstanceId;

    private void Start() {
        mainSpeed = 10;
        distanceRan = 0f;
        var resultString = Regex.Match(transform.parent.name, @"\d+").Value;
        localInstanceId = Int32.TryParse(resultString, out var teamId) ? teamId : 0;
    }
    
    public void Restart() {
        addDistance = false;
        foreach (Transform transform in transform.parent.transform) {
            if (transform.CompareTag("Obstacle") || transform.CompareTag("Cloud")) {
                Destroy(transform.gameObject);
            }
            if (transform.TryGetComponent(out Scores scores)) {
                scores.Reset();
            }
            if (transform.TryGetComponent(out MoveConstantly moveConst)) {
                moveConst.Stop();
            }
        }
        mainSpeed = 10;
        distanceRan = 0f;
        addDistance = true;
    }

    public void Update() {
        if (addDistance) {
            distanceRan += mainSpeed * Time.deltaTime / 4;
            if (mainSpeed < maxSpeed) mainSpeed += acceleration;
        }
    }

    public float getDistance() {
        return distanceRan;
    }
}