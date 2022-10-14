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

    private List<GameObject> walls;

    public void LoadFirstLevel() {
        walls = new List<GameObject>();
        
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
                for (int y = 1; y < height; y++) {
                    char cell = lines[y][x];

                    switch (cell.ToString()) {
                        case "W":
                            GameObject wall = Instantiate(wallPrefab, new Vector2(0, 0), new Quaternion(0, 0, 0, 0));
                            walls.Add(wall);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    private void SetSprite(Sprite sprite, GameObject obj) {
        Transform spriteTransform = obj.transform.Find("Sprite");
        spriteTransform.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        spriteTransform.position += new Vector3(20, 20, 0);
    }

    public void SetWallSprite(Sprite wallSprite) {
        foreach (GameObject wall in walls) {
            SetSprite(wallSprite, wall);
        }
    }

}
