using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterEffect : MonoBehaviour {
    private Material waterEffectMaterial;
    private TridentController waterPlane;

    private void OnEnable() {
        waterEffectMaterial = Resources.Load<Material>("UnderwaterEffect");
        waterPlane = GameObject.Find("TridentWater").GetComponent<TridentController>();
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest) {
        // check if camera is underwater
        float posY = transform.position.y;
        float waveHeight = waterPlane.getWaveHeight(transform.position);
        bool isCameraSubmerged = posY < waveHeight;
        
        // only apply underwater shader if we are actually underwater
        if (isCameraSubmerged) {
            Graphics.Blit(src, dest, waterEffectMaterial);
            RenderSettings.fog = true;
        } else {
            Graphics.Blit(src, dest);
            RenderSettings.fog = false;
        }
    }
}
