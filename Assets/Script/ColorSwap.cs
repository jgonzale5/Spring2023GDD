using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    //The cube we will change the color of
    public MeshRenderer Cube;

    //The materials that can be applied to the cube
    public Material[] colors = new Material[0]; 

    public void ChangeColor(int index)
    {
        //Sets the material of the cube to be equal to the index-th element of colors
        Cube.material = colors[index];
    }
}
