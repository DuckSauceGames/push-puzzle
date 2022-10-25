using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {

    private Vector2 IMPOSSIBLE_POSITION = new Vector2(1000, 1000);

    public AnimationGroup animations { private get; set; }

    public Level level { private get; set; }

    private float speed = 0f;

    private bool moving = false;

    private Direction direction = Direction.UP;

    private Vector2 startingPosition;
    private Vector2 targetPosition;
    private Vector2 oneBeforeTargetPosition;

    void Start() {
        targetPosition = IMPOSSIBLE_POSITION;
    }

    void Update() {
        if (moving) {
            speed += Time.deltaTime * 2;

            transform.position = Vector3.Lerp(startingPosition, targetPosition, speed);

            if (Mathf.RoundToInt(transform.position.x) == oneBeforeTargetPosition.x && Mathf.RoundToInt(transform.position.y) == oneBeforeTargetPosition.y) {
                level.MovePushable(targetPosition, direction);
            } else if ((Vector2) transform.position == targetPosition) {
                transform.position = new Vector2 (Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));

                moving = false;
                speed = 0f;
                targetPosition = IMPOSSIBLE_POSITION;
                animations.SetAnimation("idle/" + direction.ToString().ToLower());
                level.SetIdleAnimation(startingPosition);
            }
        } else {
            if (level.HasPush(transform.position)) {
                Direction pushDirection = level.GetDirectionalDirection(transform.position);
                Vector2 pushTarget = Level.GetPosition(transform.position, 1, pushDirection);
                if (level.GetCountTargetingPosition(pushTarget) <= 1) {
                    Move(pushDirection, 1);
                }
                level.SetActiveAnimation(transform.position);
            } else if (level.HasThrow(transform.position)) {
                Move(level.GetDirectionalDirection(transform.position), level.GetThrowDistance(transform.position));
                level.SetActiveAnimation(transform.position);
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
            oneBeforeTargetPosition = Level.GetPosition(targetPosition, 1, Directions.GetOpposite(direction));
            moving = true;
            animations.SetAnimation("move/" + direction.ToString().ToLower());
        }
    }

    public bool IsMoving() {
        return moving;
    }

    public Vector2 GetTargetPosition() {
        return targetPosition;
    }

}
