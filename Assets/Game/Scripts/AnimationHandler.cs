using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationHandler : MonoBehaviour {
    public class AnimationInfo
    {
        public int frameStart;
        public int numerOfFrames;
    };

    public int playAnimationSetNumber;

    int numbersOfTilesY = 7;
    float frameTime = 0.0f;
    float timePrFrame = 0.1f;
    int frameIndex = 0;

    public GameObject textureToAnimate;
    public List<AnimationInfo> animationSets = new List<AnimationInfo>();

	// Use this for initialization
	void Start () {
        frameTime = timePrFrame;
        animationSets.Add(new AnimationInfo() { frameStart = 2, numerOfFrames = 1 }); // Idle
        animationSets.Add(new AnimationInfo() { frameStart = 4, numerOfFrames = 3 }); // Walking
        animationSets.Add(new AnimationInfo() { frameStart = 0, numerOfFrames = 1 }); // Dead
	}
	
	// Update is called once per frame
	void Update () {
        Animate(animationSets[playAnimationSetNumber]);
	}

    void Animate(AnimationInfo animationSet)
    {
        frameTime -= Time.deltaTime;
        if (frameTime <= 0)
        {
            frameTime = timePrFrame;
            frameIndex++;
        }
        
        if (frameIndex < animationSet.frameStart)
        {
            frameIndex = animationSet.frameStart;
        }

        if (frameIndex >= (animationSet.frameStart + (animationSet.numerOfFrames)))
        {
            frameIndex = animationSet.frameStart;
        }

        float sizeY = 1.0f / numbersOfTilesY;
        Vector2 size = new Vector2(1.0f, sizeY);

        var vIndex = frameIndex / numbersOfTilesY;

        float offsetX = 1.0f;
        float offsetY = (1.0f - size.y) - (vIndex + frameIndex) * size.y;
        Vector2 offset = new Vector2(offsetX, offsetY);

        textureToAnimate.renderer.material.SetTextureOffset("_MainTex", offset);
        textureToAnimate.renderer.material.SetTextureScale("_MainTex", size);
    }
}
