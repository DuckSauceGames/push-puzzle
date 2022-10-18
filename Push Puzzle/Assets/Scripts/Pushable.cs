using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    private Vector2 IMPOSSIBLE_POSITION = new Vector2(1000, 1000);

    private Level level;

    private float speed = 0f;

    private bool moving = false;

    private Direction direction = Direction.UP;

    private Vector2 startingPosition;
    private Vector2 targetPosition;

    void Start() {
        targetPosition = IMPOSSIBLE_POSITION;
    }

    void Update() {
        if (moving) {
            speed += Time.deltaTime * 2;

            transform.position = Vector3.Lerp(startingPosition, targetPosition, speed);

            if ((Vector2) transform.position == targetPosition) {
                transform.position = new Vector2 (Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

                moving = false;
                speed = 0f;
                targetPosition = IMPOSSIBLE_POSITION;
            }
        } else {
            if (level.HasPush(transform.position)) {
                Direction pushDirection = level.GetDirectionalDirection(transform.position);
                Vector2 pushTarget = Level.GetPosition(transform.position, 1, pushDirection);
                if (level.GetCountTargetingPosition(pushTarget) <= 1) {
                    Move(pushDirection, 1);
                }
            } else if (level.HasThrow(transform.position)) {
                Move(level.GetDirectionalDirection(transform.position), level.GetThrowDistance(transform.position));
            }
        }
    }

    public void Move(Direction dir, int distance) {
        if (moving) return;

        direction = dir;

        Vector2 position = Level.GetPosition(transform.position, distance, direction);

        if (level.CanMove(position, direction)) {
            startingPosition = transform.position;
            targetPosition = position;
            moving = true;
            level.MovePushable(targetPosition, direction);
        }
    }

    public void SetLevel(Level lvl) {
        level = lvl;
    }

    public bool IsMoving() {
        return moving;
    }

    public Vector2 GetTargetPosition() {
        return targetPosition;
    }

}
