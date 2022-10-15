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
        GameObject player = Instantiate(playerPrefab, playerStartPosition, new Quaternion(0, 0, 0, 0));
        player.GetComponent<PlayerBehaviour>().SetGameBehaviour(this);
        player.GetComponent<PlayerBehaviour>().SetSprite(spriteLoader.GetPlayerSprite());

        levelLoader.SetWallSprite(spriteLoader.GetWallSprite());
    }

    public bool CanMove(Vector2 position) {
        return levelLoader.IsCellEmpty(position);
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = levelLoader.GetCenter();
        int largestLevelDimension = levelLoader.GetLargestDimension();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -largestLevelDimension);
    }

}
