public enum Direction {
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

static class Directions {

    public static Direction GetOpposite(Direction direction) {
        switch(direction) {
            case Direction.UP:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
            default:
                break;
        }

        return Direction.NONE;
    }

}