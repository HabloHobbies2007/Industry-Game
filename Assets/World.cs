using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private static World _instance;
    public static World Instance { get { return _instance; } }

    public Texture2D[] terrainTextures;
    [HideInInspector]
    public Texture2DArray terrainTexArray;

    private void Awake()
    {
        PopulateTextureArray();
        if (_instance != null && _instance != this)
        {
            Debug.LogWarning("More than one instance of World present. Removing additional instance. How'd ya mess that up, dum dum?");
            Destroy(this.gameObject);

        }
        else
            _instance = this;
    }

    void PopulateTextureArray()
    {
        terrainTexArray = new Texture2DArray(32, 32, terrainTextures.Length, TextureFormat.ARGB32, false);

        for (int i = 0; i < terrainTextures.Length; i++)
        {
            terrainTexArray.SetPixels(terrainTextures[i].GetPixels(0), i, 0);
        }

        terrainTexArray.Apply();
    }
}
