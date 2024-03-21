using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData", order = 1)]
public class SceneData : ScriptableObject
{
    // List 1 with preset values
    public List<int> list1 = new List<int>() {};

    // List 2 with preset values
    public List<int> list2 = new List<int>() {};

    // List 3 with preset values
    public List<int> list3 = new List<int>() {};

    // Vectors with preset values
    public Vector3 vector1 = new Vector3(0f, 0f, 0f);
    public Vector3 vector2 = new Vector3(0f, 0f, 0f);
    public Vector3 vector3 = new Vector3(0f, 0f, 0f);
    public Vector3 vector4 = new Vector3(0f, 0f, 0f);
}
