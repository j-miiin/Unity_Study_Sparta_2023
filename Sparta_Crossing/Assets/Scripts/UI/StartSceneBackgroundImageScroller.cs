using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneBackgroundImageScroller : MonoBehaviour    
{
    public float speed;

    private MeshRenderer render;
    private float offeset;

    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        offeset += Time.deltaTime * speed;
        render.material.mainTextureOffset = new Vector2(offeset, offeset);
    }
}
