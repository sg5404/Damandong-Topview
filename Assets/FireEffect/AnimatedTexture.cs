using UnityEngine;
using System.Collections;

public class AnimatedTexture : MonoBehaviour
{
    public float fps = 30.0f;
    public Texture2D[] frames;

    private int frameIndex;
    private MeshRenderer rendererMy;

    void Start()
    {
        rendererMy = GetComponent<MeshRenderer>();
        InvokeRepeating(nameof(NextFrame), 0, 1 / fps);
    }

    void NextFrame()
    {
        rendererMy.sharedMaterial.SetTexture("_MainTex", frames[frameIndex]);
        frameIndex = (frameIndex + 1) % frames.Length;
        if (frameIndex == frames.Length - 1)
        {
            Stop();
        }
    }

    private void Stop()
    {
        CancelInvoke("NextFrame");
        Destroy(gameObject);
    }
}