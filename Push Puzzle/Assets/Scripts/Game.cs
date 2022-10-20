using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public Camera camera;
    
    private Sprites sprites;
    private Level level;
    private UI ui;

    public bool isPaused { get; private set; }

    void Start() {
        isPaused = false;

        try {
            ui = transform.GetComponent<UI>();
            ui.game = this;
            
            sprites = transform.GetComponent<Sprites>();
            ui.sprites = sprites;
            sprites.LoadSprites();
            ui.SetSprites();

            level = transform.GetComponent<Level>();
            level.game = this;
            level.sprites = sprites;

            GoToNextLevel();
        } catch (FileNotFoundException e) {
            ui.ShowException(e);
            return;
        }
    }

    void Update() {
        if (Input.GetKeyDown("escape")) {
            TogglePause();
        }

        if (level.IsPlayerInGoal()) {
            GoToNextLevel();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;
        ui.TogglePauseScreenVisibility(isPaused);
        if (isPaused) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void GoToNextLevel() {
        try {
            level.GoToNextLevel();
        } catch (FileNotFoundException e) {
            ui.ShowException(e);
        }
        SetCameraPosition();
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = level.GetCenter();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -level.GetLargestDimension());
    }

    public void SetBackground(string backgroundName) {
        try {
            ui.SetBackground(backgroundName);
        } catch (FileNotFoundException e) {
            throw e;
        }
    }

}
