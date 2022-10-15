using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBehaviour : MonoBehaviour {

    private bool moving;
    private float speed;
    private Vector3 startingPosition;
    private Vector3 targetPosition;

    void Start() {
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
            }
        }
    }

    public void Move(Vector2 position) {
        startingPosition = transform.position;
        targetPosition = position;
        moving = true;
    }

}
