using UnityEngine;

public class Number : MonoBehaviour {
    public GameObject[] digits = { };
    public Sprite[] sprites = new Sprite[10];
    private SpriteRenderer[] digitSprites;
    private int val = -1;

    public int Value {
        get => val;
        set {
            val = value;
            if (digitSprites != null)
                for (var i = 0; i < digitSprites.Length; i++)
                    digitSprites[i].sprite = sprites[(int)(val / Mathf.Pow(10, i)) % 10];
        }
    }

    private void Start() {
        digitSprites = new SpriteRenderer[digits.Length];
        for (var i = 0; i < digits.Length; i++) digitSprites[i] = digits[i].GetComponent<SpriteRenderer>();
        Value = 0;
    }
}