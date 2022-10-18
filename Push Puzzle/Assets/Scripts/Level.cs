using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour {

    private const string LEVELS_FOLDER = "../assets/levels/";
    private const string LVL_EXTENSION = ".lvl";

    public Game game { private get; set; }
    public Sprites sprites { private get; set; }

    private GameObject player;

    public GameObject playerPrefab;
    public GameObject pushablePrefab;
    public GameObject wallPrefab;
    public GameObject goalPrefab;
    public GameObject pushPrefab;
    public GameObject throwPrefab;

    private GameObject pushables;
    private GameObject walls;
    private GameObject goals;
    private GameObject pushes;
    private GameObject throws;

    private int width;
    private int height;

    private int currentLevelNumber = 0;

    public void GoToNextLevel() {
        ClearLevel();
        currentLevelNumber++;
        LoadLVL(currentLevelNumber);
    }

    public void ClearLevel() {
        Destroy(player);

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

    private void LoadLVL(int level) {
        string filePath = LEVELS_FOLDER + level + LVL_EXTENSION;

        if (File.Exists(filePath)) {
            string fileContent; 
            using (StreamReader reader = new StreamReader(filePath)) fileContent = reader.ReadToEnd();
            string[] lines = fileContent.Split("\n");
                
            string[] dimensions = lines[0].Split("x");
            width = Convert.ToInt32(dimensions[0]);
            height = Convert.ToInt32(dimensions[1]);

            game.SetBackground(lines[1]);

            Vector2 position;

            for (int x = 0; x < width; x++) {
                for (int y = 2; y < height + 2; y++) {
                    char cell = lines[y][x];
                    position = new Vector2(x, -y);

                    switch (cell.ToString()) {
                        case "P":
                            player = Instantiate(playerPrefab, position, Quaternion.identity);
                            player.GetComponent<Pushable>().SetLevel(this);
                            SetSprite(player, sprites.player);
                            break;
                        case "p":
                            GameObject pushable = Instantiate(pushablePrefab, position, Quaternion.identity);
                            pushable.transform.parent = pushables.transform;
                            pushable.GetComponent<Pushable>().SetLevel(this);
                            SetSprite(pushable, sprites.pushable);
                            break;
                        case "W":
                            GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                            wall.transform.parent = walls.transform;
                            SetSprite(wall, sprites.wall);
                            break;
                        case "G":
                            GameObject goal = Instantiate(goalPrefab, position, Quaternion.identity);
                            goal.transform.parent = goals.transform;
                            SetSprite(goal, sprites.goal);
                            break;
                        case "U":
                            GameObject pushUp = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushUp.transform.parent = pushes.transform;
                            pushUp.GetComponent<Directional>().SetDirection(Direction.UP);
                            SetSprite(pushUp, sprites.pushUp);
                            break;
                        case "R":
                            GameObject pushRight = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushRight.transform.parent = pushes.transform;
                            pushRight.GetComponent<Directional>().SetDirection(Direction.RIGHT);
                            SetSprite(pushRight, sprites.pushRight);
                            break;
                        case "D":
                            GameObject pushDown = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushDown.transform.parent = pushes.transform;
                            pushDown.GetComponent<Directional>().SetDirection(Direction.DOWN);
                            SetSprite(pushDown, sprites.pushDown);
                            break;
                        case "L":
                            GameObject pushLeft = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushLeft.transform.parent = pushes.transform;
                            pushLeft.GetComponent<Directional>().SetDirection(Direction.LEFT);
                            SetSprite(pushLeft, sprites.pushLeft);
                            break;
                        case "^":
                            GameObject throwUp = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwUp.transform.parent = throws.transform;
                            throwUp.GetComponent<Directional>().SetDirection(Direction.UP);
                            SetSprite(throwUp, sprites.throwUp);
                            break;
                        case ">":
                            GameObject throwRight = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwRight.transform.parent = throws.transform;
                            throwRight.GetComponent<Directional>().SetDirection(Direction.RIGHT);
                            SetSprite(throwRight, sprites.throwRight);
                            break;
                        case "V":
                            GameObject throwDown = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwDown.transform.parent = throws.transform;
                            throwDown.GetComponent<Directional>().SetDirection(Direction.DOWN);
                            SetSprite(throwDown, sprites.throwDown);
                            break;
                        case "<":
                            GameObject throwLeft = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwLeft.transform.parent = throws.transform;
                            throwLeft.GetComponent<Directional>().SetDirection(Direction.LEFT);
                            SetSprite(throwLeft, sprites.throwLeft);
                            break;
                        default:
                            break;
                    }
                }
            }
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

    private bool Has(Vector2 position, GameObject parent) {
        foreach (Transform child in parent.transform) {
            if (child.position.x == position.x && child.position.y == position.y) {
                return true;
            }
        }
        return false;
    }

    public bool HasPlayer(Vector2 position) {
        return (Vector2) player.transform.position == position;
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

    public Direction GetDirectionalDirection(Vector2 position) {
        foreach (Transform push in pushes.transform) {
            if (push.position.x == position.x && push.position.y == position.y) return push.gameObject.GetComponent<Directional>().GetDirection();
        }

        foreach (Transform thro in throws.transform) {
            if (thro.position.x == position.x && thro.position.y == position.y) return thro.gameObject.GetComponent<Directional>().GetDirection();
        }

        return Direction.UP;
    }

    public bool CanMove(Vector2 position, Direction direction) {
        if (GetCountTargetingPosition(position) > 1) return false;
        if ((HasPlayer(position) || HasPushable(position)) && !CanMove(GetPosition(position, 1, direction), direction)) return false;
        return !HasWall(position);
    }

    public void MovePushable(Vector2 position, Direction direction) {
        if ((Vector2) player.transform.position == position) {
            player.GetComponent<Pushable>().Move(direction, 1);
            return;
        }

        foreach (Transform pushable in pushables.transform) {
            if ((Vector2) pushable.position == position) {
                pushable.gameObject.GetComponent<Pushable>().Move(direction, 1);
                return;
            }
        }
    }

    public int GetCountTargetingPosition(Vector2 targetPosition) {
        int count = 1;
        if (player.GetComponent<Pushable>().GetTargetPosition() == targetPosition) count++;
        foreach (Transform pushable in pushables.transform) {
            if (pushable.gameObject.GetComponent<Pushable>().GetTargetPosition() == targetPosition) count++;
        }
        return count;
    }

    public int GetThrowDistance(Vector2 position) {
        foreach (Transform thro in throws.transform) {
            if ((Vector2) thro.position == position) {
                Direction direction = thro.gameObject.GetComponent<Directional>().GetDirection();
                
                int maxDistance = 0;
                switch (direction) {
                    case Direction.UP:
                        maxDistance = Mathf.RoundToInt(-position.y) - 1;
                        break;
                    case Direction.DOWN:
                        maxDistance = (height + 1) - Mathf.RoundToInt(-position.y);
                        break;
                    case Direction.LEFT:
                        maxDistance = Mathf.RoundToInt(position.x);
                        break;
                    case Direction.RIGHT:
                        maxDistance = (width - 1) - Mathf.RoundToInt(position.x);
                        break;
                    default:
                        break;
                }

                int distance = 1;

                Vector2 potentialTarget;

                while (distance < maxDistance) {
                    potentialTarget = GetPosition(position, distance, direction);

                    if (HasWall(potentialTarget)) {
                        return distance - 1;
                    } else if (HasGoal(potentialTarget)) {
                        return distance;
                    } else if (HasPlayer(potentialTarget) || HasPushable(potentialTarget) || HasPush(potentialTarget) || HasThrow(potentialTarget)) {
                        if (CanMove(potentialTarget, direction)) {
                            return distance;
                        } else {
                            return distance - 1;
                        }
                    }

                    distance++;
                }

                return maxDistance;
            }
        }
        return 0;
    }

    public static Vector2 GetPosition(Vector2 fromPosition, int distance, Direction direction) {
        switch (direction) {
            case Direction.UP:
                fromPosition += new Vector2(0, distance);
                break;
            case Direction.DOWN:
                fromPosition += new Vector2(0, -distance);
                break;
            case Direction.LEFT:
                fromPosition += new Vector2(-distance, 0);
                break;
            case Direction.RIGHT:
                fromPosition += new Vector2(distance, 0);
                break;
            default:
                break;
        }
        return fromPosition;
    }

    public bool IsPlayerInGoal() {
        foreach (Transform goal in goals.transform) {
            if (HasPlayer(goal.position)) return true;
        }
        return false;
    }

    private void SetSprite(GameObject obj, Sprite sprite) {
        obj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

}
