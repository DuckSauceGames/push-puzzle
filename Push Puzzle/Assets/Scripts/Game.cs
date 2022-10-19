using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        try {
            sprites.LoadSprites();
            GoToNextLevel();
        } catch (FileNotFoundException e) {
            ShowErrorMessage(e);
            return;
        }
    }

    void Update() {
        if (level.IsPlayerInGoal()) {
            GoToNextLevel();
        }
    }

    public void GoToNextLevel() {
        try {
            level.GoToNextLevel();
        } catch (FileNotFoundException e) {
            ShowErrorMessage(e);
        }
        SetCameraPosition();
    }

    private void SetCameraPosition() {
        Vector2 levelCenter = level.GetCenter();
        camera.transform.position = new Vector3(levelCenter.x, levelCenter.y, -level.GetLargestDimension());
    }

    public void SetBackground(string backgroundName) {
        try {
            canvas.transform.Find("Background").GetComponent<Image>().sprite = sprites.GetBackground(backgroundName);
        } catch (FileNotFoundException e) {
            ShowErrorMessage(e);
            throw e;
        }
    }

    private void ShowErrorMessage(FileNotFoundException error) {
        canvas.transform.Find("Error Message").GetComponent<Text>().text = error.Message + ": " + error.FileName;
        canvas.transform.Find("Background").GetComponent<Image>().sprite = null;
        canvas.transform.Find("Background").GetComponent<Image>().color = new Color(0, 0, 0);
    }

}
