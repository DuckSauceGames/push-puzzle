using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public GameObject wallPrefab;

    private const string LEVELS_FOLDER = "../assets/levels/";
    private const string LVL_EXTENSION = ".lvl";

    private int currentLevelNumber;

    private int width;
    private int height;

    private GameObject walls;

    public void LoadFirstLevel() {
        walls = new GameObject("Walls");
        walls.transform.parent = transform;
        
        currentLevelNumber = 1;
        
        LoadLVL(currentLevelNumber);
    }

    private void LoadLVL(int level) {
        string filePath = LEVELS_FOLDER + level + LVL_EXTENSION;

        if (File.Exists(filePath)) {
            string fileContent; 
            using (StreamReader reader = new StreamReader(filePath)) fileContent = reader.ReadToEnd();
            string[] lines = fileContent.Split("\n");
                
            string[] dimensions = lines[0].Split("x");
            width = Convert.ToInt32(dimensions[0]);
            height = Convert.ToInt32(dimensions[1]);

            for (int x = 0; x < width; x++) {
                for (int y = 1; y < height + 1; y++) {
                    char cell = lines[y][x];

                    switch (cell.ToString()) {
                        case "W":
                            GameObject wall = Instantiate(wallPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            wall.transform.parent = walls.transform;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void SetSprite(Sprite sprite, GameObject obj) {
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void SetWallSprite(Sprite wallSprite) {
        foreach (Transform wall in walls.transform) {
            SetSprite(wallSprite, wall.gameObject);
        }
    }

    public Vector2 GetCenter() {
        float minX = 1000f;
        float maxX = 0f;
        float minY = 1000f;
        float maxY = 0f;

        foreach (Transform wall in walls.transform) {
            if (wall.position.x < minX) minX = wall.position.x;
            if (wall.position.x > maxX) maxX = wall.position.x;
            if (wall.position.y < minY) minY = wall.position.y;
            if (wall.position.y > maxY) maxY = wall.position.y;
        }

        return new Vector2((minX + maxX) / 2, (minY + maxY) / 2);
    }

    public int GetLargestDimension() {
        return width > height ? width : height;
    }

}
