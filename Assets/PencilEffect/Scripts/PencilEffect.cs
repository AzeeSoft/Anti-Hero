using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PencilEffect : MonoBehaviour
{
    [Range(0.000001f, 0.01f)] public float GradientThreshold = 0.01f;
    [Range(0.0f, 1f)] public float ColorThreshold = 0.5f;

    private Material _pencilEffectMaterial;

    // Use this for initialization
    void Start()
    {
        CreateMaterialIfNeeded();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        CreateMaterialIfNeeded();

        _pencilEffectMaterial.SetFloat("_GradThresh", GradientThreshold);
        _pencilEffectMaterial.SetFloat("_ColorThreshold", ColorThreshold);

        Graphics.Blit(src, dest, _pencilEffectMaterial);
    }

    void CreateMaterialIfNeeded()
    {
        if (_pencilEffectMaterial == null)
        {
            _pencilEffectMaterial = new Material(Shader.Find("Azee/PostRendering/Pencil"));
        }
    }
}