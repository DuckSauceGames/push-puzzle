using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class Sprites : MonoBehaviour {

    private const string SPRITES_FOLDER = "../assets/sprites/";

    private const string BACKGROUNDS_FOLDER = "backgrounds/";
    private const string UI_FOLDER = "ui/";

    private const string ANIMATION_PROPERTIES = "/animation.properties";

    private const string PNG_EXTENSION = ".png";

    public Sprite background { get; protected set; }
    public Sprite pointer { get; protected set; }
    public Sprite paused { get; protected set; }
    public Sprite resume { get; protected set; }
    public Sprite restart { get; protected set; }
    public Sprite exit { get; protected set; }

    public void LoadSprites() {
        pointer = LoadPNG(UI_FOLDER + "pointer");
        paused = LoadPNG(UI_FOLDER + "paused");
        resume = LoadPNG(UI_FOLDER + "resume");
        restart = LoadPNG(UI_FOLDER + "restart");
        exit = LoadPNG(UI_FOLDER + "exit");
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
        } else {
            throw new FileNotFoundException("Couldn't find sprite", filePath);
        }

        return sprite;
    }

    public void LoadAnimations(AnimationGroup animations, string prefabName, params string[] animationNames) {
        foreach (string animationName in animationNames) {
            LoadAnimation(animations, prefabName, animationName);
        }
        animations.SetAnimation(animationNames[0]);
    }

    private void LoadAnimation(AnimationGroup animations, string prefabName, string animationName) {
        SingleAnimation animation = LoadAnimationProperties(prefabName, animationName);

        int numberOfFrames = animation.numberOfFrames;
        Sprite[] sprites = new Sprite[numberOfFrames];

        for (int i = 1; i <= numberOfFrames; i++) {
            sprites[i - 1] = LoadPNG(prefabName + "/" + animationName + "/" + i);
        }
        animation.frames = sprites;

        animations.AddAnimation(animation);
    }

    private SingleAnimation LoadAnimationProperties(string prefabName, string animationName) {
        int numberOfFrames = 0;
        float speed = 0f;
        bool repeat = false;
        bool resetToFirstFrame = false;

        string filePath = SPRITES_FOLDER + prefabName + "/" + animationName + ANIMATION_PROPERTIES;

        if (File.Exists(filePath)) {
            string fileContent; 
            using (StreamReader reader = new StreamReader(filePath)) fileContent = reader.ReadToEnd();
            string[] lines = fileContent.Split("\n");

            for (int i = 0; i < lines.Length; i++) {
                string[] line = lines[i].Split("=");
                if (line[0] == "numberOfFrames") numberOfFrames = Convert.ToInt32(line[1]);
                if (line[0] == "speed") speed = float.Parse(line[1], CultureInfo.InvariantCulture.NumberFormat);
                if (line[0] == "repeat") repeat = bool.Parse(line[1]);
                if (line[0] == "resetToFirstFrame") resetToFirstFrame = bool.Parse(line[1]);
            }
        } else {
            throw new FileNotFoundException("Couldn't find animation properties", filePath);
        }

        return new SingleAnimation(animationName, numberOfFrames, speed, repeat, resetToFirstFrame);
    }

    public Sprite GetBackground(string fileName) {
        return LoadPNG(BACKGROUNDS_FOLDER + fileName);
    }
 
}
