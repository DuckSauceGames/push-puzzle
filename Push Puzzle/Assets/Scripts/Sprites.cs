using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sprites : MonoBehaviour {

    private const string SPRITES_FOLDER = "../assets/sprites/";
    private const string BACKGROUNDS_FOLDER = "backgrounds/";
    private const string PNG_EXTENSION = ".png";

    private Sprite background;

    public Sprite player { get; protected set; }

    public Sprite pushable { get; protected set; }

    public Sprite wall { get; protected set; }

    public Sprite goal { get; protected set; }

    public Sprite pushUp { get; protected set; }
    public Sprite pushDown { get; protected set; }
    public Sprite pushRight { get; protected set; }
    public Sprite pushLeft { get; protected set; }

    public Sprite throwUp { get; protected set; }
    public Sprite throwDown { get; protected set; }
    public Sprite throwRight { get; protected set; }
    public Sprite throwLeft { get; protected set; }

    public void LoadSprites() {
        player = LoadPNG("player");

        pushable = LoadPNG("pushable");

        wall = LoadPNG("wall");

        goal = LoadPNG("goal");

        pushUp = LoadPNG("push_up");
        pushDown = LoadPNG("push_down");
        pushRight = LoadPNG("push_right");
        pushLeft = LoadPNG("push_left");

        throwUp = LoadPNG("throw_up");
        throwDown = LoadPNG("throw_down");
        throwRight = LoadPNG("throw_right");
        throwLeft = LoadPNG("throw_left");
    }

    private Sprite LoadPNG(string fileName) {
        string filePath = SPRITES_FOLDER + fileName + PNG_EXTENSION;
        Sprite sprite = null;
        byte[] fileData;

        if (File.Exists(filePath)) {
            fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0, 0), 64);
        }

        return sprite;
    }

    public Sprite GetBackground(string fileName) {
        return LoadPNG(BACKGROUNDS_FOLDER + fileName);
    }
 
}
