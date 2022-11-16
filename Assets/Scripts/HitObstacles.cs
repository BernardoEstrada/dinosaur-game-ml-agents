using System;
using ML;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HitObstacles : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameObject restart;
    private Animator animator;
    private Vector3 restartStartPosition = Vector3.zero;
    private bool stop;
    private DinoAgent agent;
    private Animator otherAnimator;

    private void Start() {
        agent = GetComponent<DinoAgent>();
        restart = transform.parent.Find("Restart").gameObject;
        animator = GetComponent<Animator>();
        restartStartPosition = restart.transform.position;
        restart.transform.position = Vector3.up * 50;
    }

    private void Update() {
        // if (!stop || !Input.anyKeyDown) return;
        // SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }

    public void Reset() {
        animator.SetBool("hit", false);
        if (otherAnimator != null) otherAnimator.SetBool("hit", false);
        restart.transform.position = Vector3.up * 50;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Obstacle" &&
            collider.transform.parent.GetComponentInChildren<Level>().localInstanceId == transform.parent.GetComponentInChildren<Level>().localInstanceId) {
            if (audioSource && audioClip)
                audioSource.PlayOneShot(audioClip, 1);
            restart.transform.position = restartStartPosition;
            // Time.timeScale = 0;
            agent.CrashedWithObstacle();
            stop = true;
            animator.SetBool("hit", true);
            otherAnimator = collider.gameObject.GetComponent<Animator>();
            if (otherAnimator == null) return;
            otherAnimator.SetBool("hit", true);
        }
    }
}