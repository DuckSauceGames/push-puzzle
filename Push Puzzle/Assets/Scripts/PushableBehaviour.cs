using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBehaviour : MonoBehaviour {

    private LevelLoader levelLoader;

    private bool moving;
    private float speed;
    private Vector3 startingPosition;
    private Vector3 targetPosition;

    void Start() {
        startingPosition = transform.position;
        targetPosition = transform.position;
        moving = false;
        speed = 0f;
    }

    void Update() {
        if (moving) {
            speed += Time.deltaTime * 2;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, speed);
            if (transform.position == targetPosition) {
                transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), transform.position.z);
                moving = false;
                speed = 0f;

                if (levelLoader.HasPush(transform.position)) {
                    startingPosition = transform.position;
                    targetPosition = levelLoader.GetPushTarget(transform.position);
                    moving = true;
                }

                if (levelLoader.HasThrow(transform.position)) {
                    startingPosition = transform.position;
                    targetPosition = levelLoader.GetThrowTarget(transform.position);
                    moving = true;
                }
            }
        }
    }

    public Vector2 GetPosition() {
        return targetPosition;
    } 

    public void Move(Vector2 position) {
        startingPosition = transform.position;
        targetPosition = position;
        moving = true;
    }

    public void SetLevelLoader(LevelLoader ll) {
        levelLoader = ll;
    }

}
