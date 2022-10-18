using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Camera camera;
    public Canvas canvas;

    private Sprites sprites;
    private Level level;

    void Start() {
        sprites = transform.GetComponent<Sprites>();

        level = transform.GetComponent<Level>();
        level.game = this;
        level.sprites = sprites;

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
        SetCameraPosition();
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = level.GetCenter();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -level.GetLargestDimension());
    }

    public void SetBackground(string backgroundName) {
        canvas.transform.Find("Background").GetComponent<Image>().sprite = sprites.GetBackground(backgroundName);
    }

}
