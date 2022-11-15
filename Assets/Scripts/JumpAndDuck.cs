using UnityEngine;

public class JumpAndDuck : MonoBehaviour {
    public Level level;
    public GameObject ground;
    public Collider2D standingCollider;
    public Collider2D duckingCollider;
    public AudioSource jumpAudioSource;
    public AudioClip jumpAudioClip;
    private readonly float gravity = 144f;
    private Animator animator;
    private bool ducking;
    private bool grounded = true;
    private float jumpVelocity;
    private Vector3 startVector;

    private void Start() {
        animator = GetComponent<Animator>();
        standingCollider.enabled = true;
        duckingCollider.enabled = false;
    }

    private void Update() {
        if (grounded) {
            if (Input.GetButton("Jump") || Input.GetAxis("Vertical") > 0)
                jump();
            else if (Input.GetAxis("Vertical") < 0)
                duck();
            else
                stand();
        }
        else {
            transform.position += jumpVelocity * Vector3.up * Time.deltaTime;
            jumpVelocity -= gravity * Time.deltaTime;

            if (transform.position.y < ground.transform.position.y) {
                grounded = true;
                transform.position = startVector;
                animator.SetBool("jumping", false);
            }
            else if (3 < transform.position.y && 20 < jumpVelocity) {
                jumpVelocity = 20;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject == ground) {
            grounded = true;
            transform.position = startVector;
            animator.SetBool("jumping", false);
        }
    }


    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject == ground) {
            grounded = false;
            animator.SetBool("jumping", true);
        }
    }

    private void jump() {
        if (!grounded) return;

        stand();
        if (jumpAudioSource && jumpAudioClip) jumpAudioSource.PlayOneShot(jumpAudioClip, 1);
        startVector = transform.position;
        jumpVelocity = 40f + level.mainSpeed / 10f;
        grounded = false;
        animator.SetBool("jumping", true);
    }

    private void duck() {
        if (ducking || !grounded) return;

        standingCollider.enabled = false;
        duckingCollider.enabled = true;
        ducking = true;
        animator.SetBool("ducking", true);
    }

    private void stand() {
        if (!ducking) return;

        standingCollider.enabled = true;
        duckingCollider.enabled = false;
        ducking = false;
        animator.SetBool("ducking", false);
    }
}