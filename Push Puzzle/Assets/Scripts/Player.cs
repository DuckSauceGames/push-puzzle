using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Game game { private get; set; }

    private Pushable pushable;

    void Start() {
        pushable = GetComponent<Pushable>();
    }

    void Update() {
        if (game.isPaused) return;

        if (Input.GetKeyDown("w")) {
            pushable.Move(Direction.UP, 1);
        } else if (Input.GetKeyDown("a")) {
            pushable.Move(Direction.LEFT, 1);
        } else if (Input.GetKeyDown("s")) {
            pushable.Move(Direction.DOWN, 1);
        } else if (Input.GetKeyDown("d")) {
            pushable.Move(Direction.RIGHT, 1);
        }
    }

}
