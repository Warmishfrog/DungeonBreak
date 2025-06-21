using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to check if the player is looking at the angel object.
/// </summary>
public class Angel : MonoBehaviour
{
    [SerializeField] private Camera Camera;

    //Renderer is the object the camera is detecting visibility for
    [SerializeField] private Renderer Renderer;
    public bool IsVisible ;

    private void Start()
    {
        Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        IsVisible = GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera), Renderer.bounds);
    }
}
