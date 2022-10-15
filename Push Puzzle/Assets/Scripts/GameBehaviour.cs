using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    public Camera camera;

    public GameObject playerPrefab;
    private GameObject player;

    private SpriteLoader spriteLoader;
    private LevelLoader levelLoader;

    void Start() {
        spriteLoader = transform.GetComponent<SpriteLoader>();
        levelLoader = transform.GetComponent<LevelLoader>();

        spriteLoader.LoadSprites();

        levelLoader.LoadFirstLevel();

        SetCameraPosition();

        Vector2 playerStartPosition = levelLoader.GetPlayerStartPosition();
        player = Instantiate(playerPrefab, playerStartPosition, new Quaternion(0, 0, 0, 0));
        player.GetComponent<PlayerBehaviour>().SetGameBehaviour(this);

        SetSprites();
    }

    public bool IsGoal(Vector2 position) {
        return levelLoader.HasGoal(position);
    }

    public void GoToNextLevel() {
        levelLoader.GoToNextLevel();
        SetCameraPosition();
        player.GetComponent<PlayerBehaviour>().SetPosition(levelLoader.GetPlayerStartPosition());
        SetSprites();
    }

    public bool CanMove(Direction direction, Vector2 targetPosition) {
        if (levelLoader.HasPushable(targetPosition) && !levelLoader.PushableCanMove(direction, targetPosition)) return false;
        return !levelLoader.HasWall(targetPosition);
    }

    public void PushPushable(Direction direction, Vector2 position) {
        levelLoader.PushPushable(direction, position);
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = levelLoader.GetCenter();
        int largestLevelDimension = levelLoader.GetLargestDimension();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -largestLevelDimension);
    }

    private void SetSprites() {
        player.GetComponent<PlayerBehaviour>().SetSprite(spriteLoader.GetPlayerSprite());

        levelLoader.SetPushableSprite(spriteLoader.GetPushableSprite());

        levelLoader.SetWallSprite(spriteLoader.GetWallSprite());
        levelLoader.SetGoalSprite(spriteLoader.GetGoalSprite());
    }

}
