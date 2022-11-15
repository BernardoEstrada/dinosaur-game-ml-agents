using System.Collections;
using UnityEngine;

public class Scores : MonoBehaviour {
    private static float highestScore;
    public Level level;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public Number score;
    public Number highScore;
    public Number flashScore;
    public int flashIterations = 10;
    public float flashDuration = 1;
    private int iterated;
    private int lastHundred;
    private Vector3 scoreStartPosition = Vector3.zero;

    private void Awake() {
        highScore.Value = (int)highestScore;
    }

    private void Start() {
        flashScore.transform.position = score.transform.position + Vector3.up * 50;
    }

    private void Update() {
        if (audioSource && audioClip && (int)(level.getDistance() / 100) != lastHundred) {
            lastHundred = (int)(level.getDistance() / 100);
            flashScore.Value = lastHundred * 100;
            StartCoroutine(FlashScore());
        }

        score.Value = (int)level.getDistance();
        highScore.Value = (int)highestScore;
    }

    private void OnDestroy() {
        highestScore = Mathf.Max(level.getDistance(), highestScore);
    }

    private IEnumerator FlashScore() {
        scoreStartPosition = score.transform.position;
        score.transform.position = scoreStartPosition + Vector3.up * 50;
        iterated = 0;
        audioSource.PlayOneShot(audioClip, 1);
        return FlashScore(false);
    }

    private IEnumerator FlashScore(bool showScore) {
        flashScore.transform.position = scoreStartPosition + (showScore ? Vector3.zero : Vector3.up * 50);
        if (showScore) iterated++;
        if (iterated >= flashIterations) {
            score.transform.position = scoreStartPosition;
            flashScore.transform.position = scoreStartPosition + Vector3.up * 50;
        }
        else {
            yield return new WaitForSeconds(flashDuration);
            StartCoroutine(FlashScore(!showScore));
        }
    }
}