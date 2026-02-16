using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    GameObject outline;

    void Awake()
    {
        outline = new GameObject("Outline");

        outline.transform.SetParent(transform, false);
        outline.transform.localPosition = Vector3.zero;
        outline.transform.localRotation = Quaternion.identity;
        outline.transform.localScale = Vector3.one * 1.05f;

        Material mat = new Material(Shader.Find("Standard"));
        mat.color = new Color(1f, 0.9f, 0f, 0.3f);
        mat.SetFloat("_Mode", 3); // Transparent
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Renderer meshRenderer = GetComponent<Renderer>();

        MeshFilter outlineMeshFilter = outline.AddComponent<MeshFilter>();
        Renderer outlineRenderer = outline.AddComponent<MeshRenderer>();

        outlineMeshFilter.mesh = meshFilter.mesh;

        outlineRenderer.material = mat;

        outline.SetActive(false);
    }

    public void SetOutline(bool state)
    {
        outline.SetActive(state);
    }
}
