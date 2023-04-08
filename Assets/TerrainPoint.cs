using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPoint 
{
    public float dstToSurface = 1f;

    public int textureID = 0;

    public TerrainPoint (float dst, int tex)
    {
        dstToSurface = dst;
        textureID = tex;
    }
}
