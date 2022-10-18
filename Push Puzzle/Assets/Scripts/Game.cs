using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

    public Camera camera;

    private Sprites sprites;
    private Level level;

    void Start() {
        level = transform.GetComponent<Level>();

        sprites = transform.GetComponent<Sprites>();
        sprites.SetLevel(level);
        sprites.LoadSprites();

        GoToNextLevel();
    }

    void Update() {
        if (level.IsPlayerInGoal()) {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel() {
        level.GoToNextLevel();
        sprites.SetSprites();
        SetCameraPosition();
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = level.GetCenter();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -level.GetLargestDimension());
    }

}
