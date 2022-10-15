using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    public Camera camera;

    private SpriteLoader spriteLoader;
    private LevelLoader levelLoader;

    void Start() {
        spriteLoader = transform.GetComponent<SpriteLoader>();
        levelLoader = transform.GetComponent<LevelLoader>();

        spriteLoader.LoadSprites();

        levelLoader.LoadFirstLevel();

        SetCameraPosition();

        levelLoader.SetWallSprite(spriteLoader.GetWallSprite());
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = levelLoader.GetCenter();
        int largestLevelDimension = levelLoader.GetLargestDimension();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -largestLevelDimension);
    }

}
