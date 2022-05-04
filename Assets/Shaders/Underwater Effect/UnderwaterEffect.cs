using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode, ImageEffectAllowedInSceneView]
public class UnderwaterEffect : MonoBehaviour {
    private Material material;

    private void OnEnable() {
        material = Resources.Load<Material>("UnderwaterEffect");
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        Graphics.Blit(src, dest, material);
    }
}
