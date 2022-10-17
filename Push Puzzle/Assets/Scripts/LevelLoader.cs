using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelLoader : MonoBehaviour {

    private GameBehaviour gameBehaviour;

    public GameObject pushablePrefab;
    public GameObject wallPrefab;
    public GameObject goalPrefab;
    public GameObject pushPrefab;
    public GameObject throwPrefab;

    private const string LEVELS_FOLDER = "../assets/levels/";
    private const string LVL_EXTENSION = ".lvl";

    private int currentLevelNumber;

    private int width;
    private int height;

    private Vector2 playerStartPosition;

    private GameObject pushables;
    private GameObject walls;
    private GameObject goals;
    private GameObject pushes;
    private GameObject throws;

    public void SetGameBehaviour(GameBehaviour gb) {
        gameBehaviour = gb;
    }

    public void ClearLevel() {
        Destroy(pushables);
        pushables = new GameObject("Pushables");

        Destroy(walls);
        walls = new GameObject("Walls");

        Destroy(goals);
        goals = new GameObject("Goals");

        Destroy(pushes);
        pushes = new GameObject("Pushes");

        Destroy(throws);
        throws = new GameObject("Throws");
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
                            pushable.GetComponent<PushableBehaviour>().SetLevelLoader(this);
                            break;
                        case "W":
                            GameObject wall = Instantiate(wallPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            wall.transform.parent = walls.transform;
                            break;
                        case "G":
                            GameObject goal = Instantiate(goalPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            goal.transform.parent = goals.transform;
                            break;
                        case "U":
                            GameObject pushUp = Instantiate(pushPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            pushUp.transform.parent = pushes.transform;
                            pushUp.GetComponent<Directional>().SetDirection(Direction.UP);
                            break;
                        case "R":
                            GameObject pushRight = Instantiate(pushPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            pushRight.transform.parent = pushes.transform;
                            pushRight.GetComponent<Directional>().SetDirection(Direction.RIGHT);
                            break;
                        case "D":
                            GameObject pushDown = Instantiate(pushPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            pushDown.transform.parent = pushes.transform;
                            pushDown.GetComponent<Directional>().SetDirection(Direction.DOWN);
                            break;
                        case "L":
                            GameObject pushLeft = Instantiate(pushPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            pushLeft.transform.parent = pushes.transform;
                            pushLeft.GetComponent<Directional>().SetDirection(Direction.LEFT);
                            break;
                        case "^":
                            GameObject throwUp = Instantiate(throwPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            throwUp.transform.parent = throws.transform;
                            throwUp.GetComponent<Directional>().SetDirection(Direction.UP);
                            break;
                        case ">":
                            GameObject throwRight = Instantiate(throwPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            throwRight.transform.parent = throws.transform;
                            throwRight.GetComponent<Directional>().SetDirection(Direction.RIGHT);
                            break;
                        case "V":
                            GameObject throwDown = Instantiate(throwPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            throwDown.transform.parent = throws.transform;
                            throwDown.GetComponent<Directional>().SetDirection(Direction.DOWN);
                            break;
                        case "<":
                            GameObject throwLeft = Instantiate(throwPrefab, new Vector2(x, -y), new Quaternion(0, 0, 0, 0));
                            throwLeft.transform.parent = throws.transform;
                            throwLeft.GetComponent<Directional>().SetDirection(Direction.LEFT);
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

    public void SetPushUpSprite(Sprite pushUpSprite) {
        foreach (Transform push in pushes.transform) {
            if (push.gameObject.GetComponent<Directional>().GetDirection() == Direction.UP) SetSprite(pushUpSprite, push.gameObject);
        }
    }

    public void SetPushDownSprite(Sprite pushDownSprite) {
        foreach (Transform push in pushes.transform) {
            if (push.gameObject.GetComponent<Directional>().GetDirection() == Direction.DOWN) SetSprite(pushDownSprite, push.gameObject);
        }
    }

    public void SetPushLeftSprite(Sprite pushLeftSprite) {
        foreach (Transform push in pushes.transform) {
            if (push.gameObject.GetComponent<Directional>().GetDirection() == Direction.LEFT) SetSprite(pushLeftSprite, push.gameObject);
        }
    }

    public void SetPushRightSprite(Sprite pushRightSprite) {
        foreach (Transform push in pushes.transform) {
            if (push.gameObject.GetComponent<Directional>().GetDirection() == Direction.RIGHT) SetSprite(pushRightSprite, push.gameObject);
        }
    }

    public void SetThrowUpSprite(Sprite throwUpSprite) {
        foreach (Transform thro in throws.transform) {
            if (thro.gameObject.GetComponent<Directional>().GetDirection() == Direction.UP) SetSprite(throwUpSprite, thro.gameObject);
        }
    }

    public void SetThrowDownSprite(Sprite throwDownSprite) {
        foreach (Transform thro in throws.transform) {
            if (thro.gameObject.GetComponent<Directional>().GetDirection() == Direction.DOWN) SetSprite(throwDownSprite, thro.gameObject);
        }
    }

    public void SetThrowLeftSprite(Sprite throwLeftSprite) {
        foreach (Transform thro in throws.transform) {
            if (thro.gameObject.GetComponent<Directional>().GetDirection() == Direction.LEFT) SetSprite(throwLeftSprite, thro.gameObject);
        }
    }

    public void SetThrowRightSprite(Sprite throwRightSprite) {
        foreach (Transform thro in throws.transform) {
            if (thro.gameObject.GetComponent<Directional>().GetDirection() == Direction.RIGHT) SetSprite(throwRightSprite, thro.gameObject);
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

    private bool Has(Vector2 position, GameObject parent) {
        foreach (Transform child in parent.transform) {
            if (child.position.x == position.x && child.position.y == position.y) {
                return true;
            }
        }
        return false;
    }

    public bool HasPlayer(Vector2 position) {
        Vector2 playerPosition = gameBehaviour.GetPlayerPosition();
        return playerPosition.x == position.x && playerPosition.y == position.y;
    }

    public bool HasPushable(Vector2 position) {
        return Has(position, pushables);
    }

    public bool HasWall(Vector2 position) {
        return Has(position, walls);
    }

    public bool HasGoal(Vector2 position) {
        return Has(position, goals);
    }

    public bool HasPush(Vector2 position) {
        return Has(position, pushes);
    }

    public bool HasThrow(Vector2 position) {
        return Has(position, throws);
    }

    public bool HasPlayerBlocker(Vector2 position) {
        return HasWall(position);
    }

    public bool HasPushableBlocker(Vector2 position) {
        return HasWall(position) || HasPlayer(position) || HasPushable(position);
    }

    public bool HasStopper(Vector2 position) {
        return HasGoal(position) || HasPush(position) || HasThrow(position);
    }

    public bool PushableCanMove(Direction direction, Vector2 position) {
        foreach (Transform pushable in pushables.transform) {
            if (pushable.position.x == position.x && pushable.position.y == position.y) {
                Vector2 targetPosition = position;
                switch (direction) {
                    case Direction.UP:
                        targetPosition = new Vector2(position.x, position.y + 1);
                        break;
                    case Direction.DOWN:
                        targetPosition = new Vector2(position.x, position.y - 1);
                        break;
                    case Direction.LEFT:
                        targetPosition = new Vector2(position.x - 1, position.y);
                        break;
                    case Direction.RIGHT:
                        targetPosition = new Vector2(position.x + 1, position.y);
                        break;
                    default:
                        break;
                }
                return !HasPushableBlocker(targetPosition);
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
                    break;
                }
            }
        }
    }

    public Vector2 GetPushTarget(Vector2 position) {
        Vector2 targetPosition = position;
        if (HasPush(position)) {
            foreach (Transform push in pushes.transform) {
                if (push.position.x == position.x && push.position.y == position.y) {
                    switch (push.gameObject.GetComponent<Directional>().GetDirection()) {
                        case Direction.UP:
                            targetPosition = new Vector2(position.x, position.y + 1);
                            break;
                        case Direction.DOWN:
                            targetPosition = new Vector2(position.x, position.y - 1);
                            break;
                        case Direction.LEFT:
                            targetPosition = new Vector2(position.x - 1, position.y);
                            break;
                        case Direction.RIGHT:
                            targetPosition = new Vector2(position.x + 1, position.y);
                            break;
                        default:
                            break;
                    }
                    if (!HasPushableBlocker(targetPosition)) return targetPosition;
                    return position;
                }
            }
        }
        return position;
    }

    public Vector2 GetThrowTarget(Vector2 position) {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);
        Vector2 targetPosition = new Vector2(x, y);
        Vector2 maybeTargetPosition;
        if (HasThrow(position)) {
            foreach (Transform thro in throws.transform) {
                if (thro.position.x == position.x && thro.position.y == position.y) {
                    switch (thro.gameObject.GetComponent<Directional>().GetDirection()) {
                        case Direction.UP:
                            while (y < 0) {
                                maybeTargetPosition = new Vector2(targetPosition.x, targetPosition.y + 1);
                                if (HasPushableBlocker(maybeTargetPosition)) return targetPosition;
                                if (HasStopper(maybeTargetPosition) && HasPushable(maybeTargetPosition)) return targetPosition;
                                y++;
                                targetPosition = new Vector2(x, y);
                                if (HasStopper(targetPosition)) return targetPosition;
                                if (HasPushable(targetPosition)) return targetPosition;
                            }
                            break;
                        case Direction.DOWN:
                            while (y > -height) {
                                maybeTargetPosition = new Vector2(targetPosition.x, targetPosition.y - 1);
                                if (HasPushableBlocker(maybeTargetPosition)) return targetPosition;
                                if (HasStopper(maybeTargetPosition) && HasPushable(maybeTargetPosition)) return targetPosition;
                                y--;
                                targetPosition = new Vector2(x, y);
                                if (HasStopper(targetPosition)) return targetPosition;
                                if (HasPushable(targetPosition)) return targetPosition;
                            }
                            break;
                        case Direction.LEFT:
                            while (x > 0) {
                                maybeTargetPosition = new Vector2(targetPosition.x - 1, targetPosition.y);
                                if (HasPushableBlocker(maybeTargetPosition)) return targetPosition;
                                if (HasStopper(maybeTargetPosition) && HasPushable(maybeTargetPosition)) return targetPosition;
                                x--;
                                targetPosition = new Vector2(x, y);
                                if (HasStopper(targetPosition)) return targetPosition;
                                if (HasPushable(targetPosition)) return targetPosition;
                            }
                            break;
                        case Direction.RIGHT:
                            while (x < width) {
                                maybeTargetPosition = new Vector2(targetPosition.x + 1, targetPosition.y);
                                if (HasPushableBlocker(maybeTargetPosition)) return targetPosition;
                                if (HasStopper(maybeTargetPosition) && HasPushable(maybeTargetPosition)) return targetPosition;
                                x++;
                                targetPosition = new Vector2(x, y);
                                if (HasStopper(targetPosition)) return targetPosition;
                                if (HasPushable(targetPosition)) return targetPosition;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        return targetPosition;
    }

    public Direction GetDirectionalDirection(Vector2 position) {
        foreach (Transform push in pushes.transform) {
            if (push.position.x == position.x && push.position.y == position.y) return push.gameObject.GetComponent<Directional>().GetDirection();
        }

        foreach (Transform thro in throws.transform) {
            if (thro.position.x == position.x && thro.position.y == position.y) return thro.gameObject.GetComponent<Directional>().GetDirection();
        }

        return Direction.UP;
    }

}
