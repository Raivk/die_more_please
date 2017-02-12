using UnityEngine;
using System.Collections;

public class MainMenuSpawnable : MonoBehaviour {

    public Sprite[] sprites;

    public SpriteRenderer spr;

    public Color c1;
    public Color c2;

    [ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
    public Color light1;

    [ColorUsage(true, true, 0f, 8f, 0.125f, 3f)]
    public Color light2;

    public SpriteGlow sg;

    private void Start()
    {
        if(Random.Range(0, 10) < 5)
        {
            spr.color = c1;
            sg.GlowColor = light1;
        }
        else
        {
            spr.color = c2;
            sg.GlowColor = light2;
        }
        spr.sprite = sprites[Random.Range(0, sprites.Length)];
        if(Random.Range(0, 10) < 5)
        {
            transform.Rotate(0, 180, 10);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
