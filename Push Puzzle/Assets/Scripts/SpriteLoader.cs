using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteLoader : MonoBehaviour {

    private const string SPRITES_FOLDER = "../assets/sprites/";
    private const string PNG_EXTENSION = ".png";

    private Sprite playerSprite;
    private Sprite pushableSprite;

    private Sprite throwUpSprite;
    private Sprite throwDownSprite;
    private Sprite throwRightSprite;
    private Sprite throwLeftSprite;

    private Sprite wallSprite;
    private Sprite goalSprite;

    public void LoadSprites() {
        playerSprite = LoadPNG("player");
        pushableSprite = LoadPNG("pushable");

        throwUpSprite = LoadPNG("throw_up");
        throwDownSprite = LoadPNG("throw_down");
        throwRightSprite = LoadPNG("throw_right");
        throwLeftSprite = LoadPNG("throw_left");

        wallSprite = LoadPNG("wall");
        goalSprite = LoadPNG("goal");
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

    public Sprite GetPlayerSprite() {
        return playerSprite;
    }

    public Sprite GetPushableSprite() {
        return pushableSprite;
    }

    public Sprite GetThrowUpSprite() {
        return throwUpSprite;
    }

    public Sprite GetThrowDownSprite() {
        return throwDownSprite;
    }

    public Sprite GetThrowRightSprite() {
        return throwRightSprite;
    }

    public Sprite GetThrowLeftSprite() {
        return throwLeftSprite;
    }

    public Sprite GetWallSprite() {
        return wallSprite;
    }

    public Sprite GetGoalSprite() {
        return goalSprite;
    }
 
}
