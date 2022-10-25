using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationGroup : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    private List<SingleAnimation> animations = new List<SingleAnimation>();
    private SingleAnimation currentAnimation;

    private int currentFrame = 0;
    private float timeElapsed = 0f;

    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!currentAnimation.repeat && currentFrame >= currentAnimation.numberOfFrames) return;

        timeElapsed += Time.deltaTime;
        if (timeElapsed >= currentAnimation.speed) {
            currentFrame++;
            if (currentFrame >= currentAnimation.numberOfFrames) {
                currentFrame = 0;
            }

            timeElapsed = 0f;
        }

        spriteRenderer.sprite = currentAnimation.frames[currentFrame];
    }

    public void SetAnimation(string name){
        currentAnimation = animations.Find(a => a.name == name);
        if (currentAnimation.resetToFirstFrame) currentFrame = 0;
    }

    public void AddAnimation(SingleAnimation newAnimation) {
        animations.Add(newAnimation);
    }

}
