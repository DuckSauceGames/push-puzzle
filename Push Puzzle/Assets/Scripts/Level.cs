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

    public void RestartLevel() {
        ClearLevel();
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

            try {
                game.SetBackground(lines[1]);
            } catch {
                return;
            }

            Vector2 position;

            for (int x = 0; x < width; x++) {
                for (int y = 2; y < height + 2; y++) {
                    string cell = lines[y].Split(",")[x].Trim();
                    position = new Vector2(x, -y);

                    switch (cell.ToString()) {
                        case "P":
                            player = Instantiate(playerPrefab, position, Quaternion.identity);
                            player.AddComponent<AnimationGroup>();
                            player.GetComponent<Player>().game = game;
                            player.GetComponent<Pushable>().level = this;
                            player.GetComponent<Pushable>().animations = player.GetComponent<AnimationGroup>();
                            sprites.LoadAnimations(
                                player.GetComponent<AnimationGroup>(), "player",
                                "idle/up", "idle/down", "idle/left", "idle/right",
                                "move/up", "move/down", "move/left", "move/right",
                                "reached_goal"
                            );
                            break;

                        case "p":
                            GameObject pushable = Instantiate(pushablePrefab, position, Quaternion.identity);
                            pushable.transform.parent = pushables.transform;
                            pushable.AddComponent<AnimationGroup>();
                            pushable.GetComponent<Pushable>().level = this;
                            pushable.GetComponent<Pushable>().animations = pushable.GetComponent<AnimationGroup>();
                            sprites.LoadAnimations(
                                pushable.GetComponent<AnimationGroup>(), "pushable",
                                "idle/up", "idle/down", "idle/left", "idle/right",
                                "move/up", "move/down", "move/left", "move/right"
                            );
                            break;

                        case "W":
                            GameObject wall = Instantiate(wallPrefab, position, Quaternion.identity);
                            wall.transform.parent = walls.transform;
                            wall.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(wall.GetComponent<AnimationGroup>(), "wall", "normal");
                            break;

                        case "G":
                            GameObject goal = Instantiate(goalPrefab, position, Quaternion.identity);
                            goal.transform.parent = goals.transform;
                            goal.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(goal.GetComponent<AnimationGroup>(), "goal", "untouched", "touched");
                            break;

                        case "U":
                            GameObject pushUp = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushUp.transform.parent = pushes.transform;
                            pushUp.GetComponent<Directional>().direction = Direction.UP;
                            pushUp.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(pushUp.GetComponent<AnimationGroup>(), "push", "idle/up", "active/up");
                            break;
                        case "R":
                            GameObject pushRight = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushRight.transform.parent = pushes.transform;
                            pushRight.GetComponent<Directional>().direction = Direction.RIGHT;
                            pushRight.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(pushRight.GetComponent<AnimationGroup>(), "push", "idle/right", "active/right");
                            break;
                        case "D":
                            GameObject pushDown = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushDown.transform.parent = pushes.transform;
                            pushDown.GetComponent<Directional>().direction = Direction.DOWN;
                            pushDown.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(pushDown.GetComponent<AnimationGroup>(), "push", "idle/down", "active/down");
                            break;
                        case "L":
                            GameObject pushLeft = Instantiate(pushPrefab, position, Quaternion.identity);
                            pushLeft.transform.parent = pushes.transform;
                            pushLeft.GetComponent<Directional>().direction = Direction.LEFT;
                            pushLeft.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(pushLeft.GetComponent<AnimationGroup>(), "push", "idle/left", "active/left");
                            break;

                        case "^":
                            GameObject throwUp = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwUp.transform.parent = throws.transform;
                            throwUp.GetComponent<Directional>().direction = Direction.UP;
                            throwUp.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(throwUp.GetComponent<AnimationGroup>(), "throw", "idle/up", "active/up");
                            break;
                        case ">":
                            GameObject throwRight = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwRight.transform.parent = throws.transform;
                            throwRight.GetComponent<Directional>().direction = Direction.RIGHT;
                            throwRight.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(throwRight.GetComponent<AnimationGroup>(), "throw", "idle/right", "active/right");
                            break;
                        case "V":
                            GameObject throwDown = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwDown.transform.parent = throws.transform;
                            throwDown.GetComponent<Directional>().direction = Direction.DOWN;
                            throwDown.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(throwDown.GetComponent<AnimationGroup>(), "throw", "idle/down", "active/down");
                            break;
                        case "<":
                            GameObject throwLeft = Instantiate(throwPrefab, position, Quaternion.identity);
                            throwLeft.transform.parent = throws.transform;
                            throwLeft.GetComponent<Directional>().direction = Direction.LEFT;
                            throwLeft.AddComponent<AnimationGroup>();
                            sprites.LoadAnimations(throwLeft.GetComponent<AnimationGroup>(), "throw", "idle/left", "active/left");
                            break;

                        default:
                            break;
                    }
                }
            }
        } else {
            throw new FileNotFoundException("Couldn't find level", filePath);
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

        return new Vector2(((minX + maxX) / 2) + 0.5f, ((minY + maxY) / 2) - 0.5f);
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
            if (push.position.x == position.x && push.position.y == position.y) return push.gameObject.GetComponent<Directional>().direction;
        }

        foreach (Transform thro in throws.transform) {
            if (thro.position.x == position.x && thro.position.y == position.y) return thro.gameObject.GetComponent<Directional>().direction;
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
                Direction direction = thro.gameObject.GetComponent<Directional>().direction;
                
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

                while (distance <= maxDistance) {
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

    public void SetIdleAnimation(Vector2 position) {
        foreach (Transform push in pushes.transform) {
            if (push.position.x == position.x && push.position.y == position.y) {
                push.gameObject.GetComponent<AnimationGroup>().SetAnimation("idle/" + push.gameObject.GetComponent<Directional>().direction.ToString().ToLower());
                return;
            }
        }

        foreach (Transform thro in throws.transform) {
            if (thro.position.x == position.x && thro.position.y == position.y) {
                thro.gameObject.GetComponent<AnimationGroup>().SetAnimation("idle/" + thro.gameObject.GetComponent<Directional>().direction.ToString().ToLower());
                return;
            }
        }
    }

    public void SetActiveAnimation(Vector2 position) {
        foreach (Transform push in pushes.transform) {
            if (push.position.x == position.x && push.position.y == position.y) {
                push.gameObject.GetComponent<AnimationGroup>().SetAnimation("active/" + push.gameObject.GetComponent<Directional>().direction.ToString().ToLower());
                return;
            }
        }

        foreach (Transform thro in throws.transform) {
            if (thro.position.x == position.x && thro.position.y == position.y) {
                thro.gameObject.GetComponent<AnimationGroup>().SetAnimation("active/" + thro.gameObject.GetComponent<Directional>().direction.ToString().ToLower());
                return;
            }
        }
    }

}
