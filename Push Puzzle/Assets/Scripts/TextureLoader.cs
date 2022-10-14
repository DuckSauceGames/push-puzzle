using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TextureLoader : MonoBehaviour {

    private Sprite wallSprite;

    private List<GameObject> walls;
    
    void Start() {
        walls = new List<GameObject>();
        walls.Add(GameObject.Find("Wall"));

        LoadPNGs();

        SetTextures();
    }

    private void LoadPNGs() {
        wallSprite = LoadPNG("wall");
    }

    private Sprite LoadPNG(string fileName) {
        string filePath = "../assets/" + fileName + ".png";
        Sprite sprite = null;
        byte[] fileData;

        if (File.Exists(filePath)) {
            fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
            sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2));
        }

        return sprite;
    }

    private void SetTextures() {
        Transform spriteTransform;

        foreach (GameObject wall in walls) {
            spriteTransform = wall.transform.Find("Sprite");
            spriteTransform.gameObject.GetComponent<SpriteRenderer>().sprite = wallSprite;
            spriteTransform.position += new Vector3(20, 20, 0);
        }
    }
 
}
