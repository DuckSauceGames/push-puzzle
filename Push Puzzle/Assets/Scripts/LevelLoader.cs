using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    public GameObject pushablePrefab;
    public GameObject wallPrefab;
    public GameObject goalPrefab;

    private const string LEVELS_FOLDER = "../assets/levels/";
    private const string LVL_EXTENSION = ".lvl";

    private int currentLevelNumber;

    private int width;
    private int height;

    private Vector2 playerStartPosition;

    private GameObject pushables;
    private GameObject walls;
    private GameObject goals;

    public void ClearLevel() {
        Destroy(pushables);
        pushables = new GameObject("Pushables");

        Destroy(walls);
        walls = new GameObject("Walls");

        Destroy(goals);
        goals = new GameObject("Goals");
    }

    public void LoadFirstLevel() {
        ClearLevel();

        currentLevelNumber = 1;
        
        LoadLVL(currentLevelNumber);
    }

    public void GoToNextLevel() {
        ClearLevel();

        currentLevelNumber++;

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
                        case "P":
                            playerStartPosition = new Vector2(x, -y);
                            break;
                        case "p":
                            GameObject pushable = Instantiate(pushablePrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            pushable.transform.parent = pushables.transform;
                            break;
                        case "W":
                            GameObject wall = Instantiate(wallPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            wall.transform.parent = walls.transform;
                            break;
                        case "G":
                            GameObject goal = Instantiate(goalPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            goal.transform.parent = goals.transform;
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

    public void SetPushableSprite(Sprite pushableSprite) {
        foreach (Transform pushable in pushables.transform) {
            SetSprite(pushableSprite, pushable.gameObject);
        }
    }

    public void SetWallSprite(Sprite wallSprite) {
        foreach (Transform wall in walls.transform) {
            SetSprite(wallSprite, wall.gameObject);
        }
    }

    public void SetGoalSprite(Sprite goalSprite) {
        foreach (Transform goal in goals.transform) {
            SetSprite(goalSprite, goal.gameObject);
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

    public Vector2 GetPlayerStartPosition() {
        return playerStartPosition;
    }

    public bool HasPushable(Vector2 position) {
        foreach (Transform pushable in pushables.transform) {
            if (pushable.position.x == position.x && pushable.position.y == position.y) {
                return true;
            }
        }
        return false;
    }

    public bool HasWall(Vector2 position) {
        foreach (Transform wall in walls.transform) {
            if (wall.position.x == position.x && wall.position.y == position.y) {
                return true;
            }
        }
        return false;
    }

    public bool HasGoal(Vector2 position) {
        foreach (Transform goal in goals.transform) {
            if (goal.position.x == position.x && goal.position.y == position.y) {
                return true;
            }
        }
        return false;
    }

    public bool HasPushBlocker(Vector2 position) {
        return HasPushable(position) || HasWall(position);
    }

    public bool PushableCanMove(Direction direction, Vector2 position) {
        if (HasPushable(position)) {
            foreach (Transform pushable in pushables.transform) {
                if (pushable.position.x == position.x && pushable.position.y == position.y) {
                    switch (direction) {
                        case Direction.UP:
                            return !HasPushBlocker(new Vector2(position.x, position.y + 1));
                        case Direction.DOWN:
                            return !HasPushBlocker(new Vector2(position.x, position.y - 1));
                        case Direction.LEFT:
                            return !HasPushBlocker(new Vector2(position.x - 1, position.y));
                        case Direction.RIGHT:
                            return !HasPushBlocker(new Vector2(position.x + 1, position.y));
                        default:
                            break;
                    }
                }
            }
        }
        return true;
    }

    public void PushPushable(Direction direction, Vector2 position) {
        if (PushableCanMove(direction, position)) {
            foreach (Transform pushable in pushables.transform) {
                if (pushable.position.x == position.x && pushable.position.y == position.y) {
                    switch (direction) {
                        case Direction.UP:
                            pushable.gameObject.GetComponent<PushableBehaviour>().Move(new Vector2(position.x, position.y + 1));
                            break;
                        case Direction.DOWN:
                            pushable.gameObject.GetComponent<PushableBehaviour>().Move(new Vector2(position.x, position.y - 1));
                            break;
                        case Direction.LEFT:
                            pushable.gameObject.GetComponent<PushableBehaviour>().Move(new Vector2(position.x - 1, position.y));
                            break;
                        case Direction.RIGHT:
                            pushable.gameObject.GetComponent<PushableBehaviour>().Move(new Vector2(position.x + 1, position.y));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

}
