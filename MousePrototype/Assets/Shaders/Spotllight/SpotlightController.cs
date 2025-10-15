using UnityEngine;

public class SpotlightController : MonoBehaviour
{
     [Header("Shader and Material Settings")]
    public Material spotlightMaterial;  // The material using your Spotlight Shader Graph
    public string positionProperty = "_Spotlight_Position"; // Match the shader property name

    [Header("Spotlight Settings")]
    public Transform spotlightTarget;  // The object whose position drives the spotlight
    public float heightOffset = 0.1f;  // Optional offset above surface

    void Update()
    {
        if (spotlightMaterial == null || spotlightTarget == null)
            return;

        // Get the world position of the spotlight target
        Vector3 spotlightPos = spotlightTarget.position + Vector3.up * heightOffset;

        // Pass it to the shader
        spotlightMaterial.SetVector(positionProperty, spotlightPos);
    }
}