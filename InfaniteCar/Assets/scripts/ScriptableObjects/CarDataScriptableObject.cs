
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CarDataScriptableObject", order = 1)]
public class CarDataScriptableObject : ScriptableObject
{
    public string CarType;
    [Range(0.0f, 1.0f)]
    public float Speed;

    [Range(1.0f, 4.0f)]
    public float Grip;

    [Range(0.01f, .1f)]
    public float Acceleration;



}
