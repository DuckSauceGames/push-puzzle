using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBehaviour : MonoBehaviour {

    private Direction direction;

    public void SetDirection(Direction dir) {
        direction = dir;
    }

    public Direction GetDirection() {
        return direction;
    }

}
