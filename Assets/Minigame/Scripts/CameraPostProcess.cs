using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraPostProcess: MonoBehaviour
{
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (material == null)
        {
            Graphics.Blit(source, destination); return;
        }

        Graphics.Blit(source, destination,material);
    }

    public void SetMaterialProperty(float VRadius, float VSoft)
    {
        material.SetFloat("_VRadius", VRadius);
        material.SetFloat("_VSoft", VSoft);
    }
}
