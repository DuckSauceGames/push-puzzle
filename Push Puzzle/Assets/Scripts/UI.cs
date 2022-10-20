using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Game game { private get; set; }
    public Sprites sprites { private get; set; }

    public Canvas foregroundCanvas;
    public Canvas backgroundCanvas;

    void Update() {
        if (game.isPaused) {
            float resumeY = foregroundCanvas.transform.Find("Pause Screen").Find("Resume").position.y;
            float exitY = foregroundCanvas.transform.Find("Pause Screen").Find("Exit").position.y;

            Transform pointer = foregroundCanvas.transform.Find("Pause Screen").Find("Pointer");
            float newYPosition = pointer.position.y;
            if (Input.GetKeyDown("w")) newYPosition = resumeY;
            else if (Input.GetKeyDown("s")) newYPosition = exitY;
            pointer.position = new Vector3(pointer.position.x, newYPosition, pointer.position.z);

            if (Input.GetKeyDown("return") || Input.GetKeyDown("e")) {
                if (pointer.position.y == resumeY) game.TogglePause();
                else if (pointer.position.y == exitY) Application.Quit();
            }
        }
    }

    public void SetBackground(string backgroundName) {
        try {
            backgroundCanvas.transform.Find("Background").GetComponent<Image>().sprite = sprites.GetBackground(backgroundName);
        } catch (FileNotFoundException e) {
            ShowException(e);
            throw e;
        }
    }

    public void SetSprites() {
        Transform pauseScreen = foregroundCanvas.transform.Find("Pause Screen");
        pauseScreen.Find("Pointer").GetComponent<Image>().sprite = sprites.pointer;
        pauseScreen.Find("Paused").GetComponent<Image>().sprite = sprites.paused;
        pauseScreen.Find("Resume").GetComponent<Image>().sprite = sprites.resume;
        pauseScreen.Find("Exit").GetComponent<Image>().sprite = sprites.exit;
    }

    public void TogglePauseScreenVisibility(bool isPaused) {
        Transform pauseScreen = foregroundCanvas.transform.Find("Pause Screen");
        pauseScreen.Find("Pointer").GetComponent<Image>().enabled = isPaused;
        pauseScreen.Find("Paused").GetComponent<Image>().enabled = isPaused;
        pauseScreen.Find("Resume").GetComponent<Image>().enabled = isPaused;
        pauseScreen.Find("Exit").GetComponent<Image>().enabled = isPaused;
    }

    public void ShowException(FileNotFoundException exception) {
        foregroundCanvas.transform.Find("Error Message").GetComponent<Text>().text = exception.Message + ": " + exception.FileName;
        backgroundCanvas.transform.Find("Background").GetComponent<Image>().sprite = null;
        backgroundCanvas.transform.Find("Background").GetComponent<Image>().color = new Color(0, 0, 0);
    }
}
