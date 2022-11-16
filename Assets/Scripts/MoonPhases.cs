using UnityEngine;
using UnityEngine.Serialization;

public class MoonPhases : MonoBehaviour {
    public TimeOfDay timeOfDay;
    [FormerlySerializedAs("renderer")] public SpriteRenderer moonSpriteRenderer;
    public Sprite[] sprites;
    private bool isNight;
    private int phase;

    private void Start() {
        if (sprites.Length > 0) {
            phase = sprites.Length - 1;
            moonSpriteRenderer.sprite = sprites[phase];
        }
    }

    private void Update() {
        if (sprites.Length > 0)
            if (!isNight && timeOfDay.isNight()) {
                phase = (phase + 1) % sprites.Length;
                moonSpriteRenderer.sprite = sprites[phase];
            }

        isNight = timeOfDay.isNight();
        moonSpriteRenderer.color = new Color(0.7333333333f, 0.7333333333f, 0.7333333333f, timeOfDay.value());
    }
}