using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowScript : MonoBehaviour
{
    GameObject shadowMaterial;

    public Material material;
    public Vector3 offset = new Vector3(10f, 0f);

    void Start()
    {
        shadowMaterial = new GameObject("ShadowMaterial");
        shadowMaterial.transform.parent = transform;

        shadowMaterial.transform.localPosition = offset;
        shadowMaterial.transform.localRotation = Quaternion.identity;

        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer sRender = shadowMaterial.AddComponent<SpriteRenderer>();

        sRender.sprite = sRenderer.sprite;
        sRender.material = material;

        sRender.sortingLayerName = sRenderer.sortingLayerName;
        sRender.sortingOrder = 0;
    }

    void LocalUpdate()
    {
        shadowMaterial.transform.localPosition = offset;
    }
}
