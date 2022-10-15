using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private GameBehaviour gameBehaviour;

    private bool moving;
    private float speed;
    private Vector3 startingPosition;
    private Vector3 targetPosition;

    void Start() {
        moving = false;
        speed = 0f;
    }

    void Update() {
        if (!moving) {
            startingPosition = transform.position;
            targetPosition = transform.position;
            Direction direction = Direction.UP;
            if (Input.GetKeyDown("w")) {
                targetPosition = transform.position + new Vector3( 0,  1,  0);
                direction = Direction.UP;
            } else if (Input.GetKeyDown("a")) {
                targetPosition = transform.position + new Vector3(-1,  0,  0);
                direction = Direction.LEFT;
            } else if (Input.GetKeyDown("s")) {
                targetPosition = transform.position + new Vector3( 0, -1,  0);
                direction = Direction.DOWN;
            } else if (Input.GetKeyDown("d")) {
                targetPosition = transform.position + new Vector3( 1,  0,  0);
                direction = Direction.RIGHT;
            }

            if (targetPosition != startingPosition && gameBehaviour.CanMove(direction, targetPosition)) {
                gameBehaviour.PushPushable(direction, targetPosition);
                moving = true;
            }
        } else {
            speed += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, speed);
            if (transform.position == targetPosition) {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
                moving = false;
                speed = 0f;
            }
        }
    }
    
    public void SetSprite(Sprite sprite) {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
    }

    public void SetGameBehaviour(GameBehaviour gb) {
        gameBehaviour = gb;
    }

}
