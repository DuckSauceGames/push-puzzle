using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAnimation {

    public string name { get; private set; }
    public int numberOfFrames { get; private set; }
    public float speed { get; private set; }
    public bool repeat { get; private set; }
    public bool resetToFirstFrame { get; private set; }

    public Sprite[] frames { get; set; }

    public SingleAnimation(string name, int numberOfFrames, float speed, bool repeat, bool resetToFirstFrame) {
        this.name = name;
        this.numberOfFrames = numberOfFrames;
        this.speed = speed;
        this.repeat = repeat;
        this.resetToFirstFrame = resetToFirstFrame;
    }
    
}
