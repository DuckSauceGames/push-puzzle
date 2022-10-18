using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Sprites : MonoBehaviour {

    private const string SPRITES_FOLDER = "../assets/sprites/";
    private const string PNG_EXTENSION = ".png";

    private Level level;

    private Sprite playerSprite;

    private Sprite pushableSprite;

    private Sprite wallSprite;

    private Sprite goalSprite;

    private Sprite pushUpSprite;
    private Sprite pushDownSprite;
    private Sprite pushRightSprite;
    private Sprite pushLeftSprite;

    private Sprite throwUpSprite;
    private Sprite throwDownSprite;
    private Sprite throwRightSprite;
    private Sprite throwLeftSprite;

    public void LoadSprites() {
        playerSprite = LoadPNG("player");

        pushableSprite = LoadPNG("pushable");

        wallSprite = LoadPNG("wall");

        goalSprite = LoadPNG("goal");

        pushUpSprite = LoadPNG("push_up");
        pushDownSprite = LoadPNG("push_down");
        pushRightSprite = LoadPNG("push_right");
        pushLeftSprite = LoadPNG("push_left");

        throwUpSprite = LoadPNG("throw_up");
        throwDownSprite = LoadPNG("throw_down");
        throwRightSprite = LoadPNG("throw_right");
        throwLeftSprite = LoadPNG("throw_left");
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

    public void SetLevel(Level lvl) {
        level = lvl;
    }

    public void SetSprites() {
        level.SetPlayerSprite(playerSprite);

        level.SetPushableSprite(pushableSprite);

        level.SetWallSprite(wallSprite);

        level.SetGoalSprite(goalSprite);

        level.SetPushUpSprite(pushUpSprite);
        level.SetPushDownSprite(pushDownSprite);
        level.SetPushRightSprite(pushRightSprite);
        level.SetPushLeftSprite(pushLeftSprite);

        level.SetThrowUpSprite(throwUpSprite);
        level.SetThrowDownSprite(throwDownSprite);
        level.SetThrowRightSprite(throwRightSprite);
        level.SetThrowLeftSprite(throwLeftSprite);
    }
 
}
