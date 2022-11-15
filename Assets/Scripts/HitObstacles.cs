using UnityEngine;
using UnityEngine.SceneManagement;

public class HitObstacles : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject restart;
    private Animator animator;
    private Vector3 restartStartPosition = Vector3.zero;
    private bool stop;

    private void Start() {
        animator = GetComponent<Animator>();
        restartStartPosition = restart.transform.position;
        restart.transform.position = Vector3.up * 50;
    }

    private void Update() {
        if (!stop || !Input.anyKeyDown) return;
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (audioSource && audioClip) audioSource.PlayOneShot(audioClip, 1);
        restart.transform.position = restartStartPosition;
        Time.timeScale = 0;
        stop = true;
        animator.SetTrigger("hit");
        var otherAnimator = collider.gameObject.GetComponent<Animator>();
        if (otherAnimator == null) return;
        otherAnimator.SetTrigger("hit");
    }
}