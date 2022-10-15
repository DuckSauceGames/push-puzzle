using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpriteLoader : MonoBehaviour {

    private const string SPRITES_FOLDER = "../assets/sprites/";
    private const string PNG_EXTENSION = ".png";

    private Sprite playerSprite;
    private Sprite pushableSprite;

    private Sprite wallSprite;

    public void LoadSprites() {
        playerSprite = LoadPNG("player");
        pushableSprite = LoadPNG("pushable");

        wallSprite = LoadPNG("wall");
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

    public Sprite GetWallSprite() {
        return wallSprite;
    }
 
}
