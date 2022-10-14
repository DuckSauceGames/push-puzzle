using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour {

    private SpriteLoader spriteLoader;
    private LevelLoader levelLoader;

    void Start() {
        spriteLoader = transform.GetComponent<SpriteLoader>();
        levelLoader = transform.GetComponent<LevelLoader>();

        spriteLoader.LoadSprites();

        levelLoader.LoadFirstLevel();

        levelLoader.SetWallSprite(spriteLoader.GetWallSprite());
    }

}
