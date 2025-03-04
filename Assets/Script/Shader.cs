using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class Shader : MonoBehaviour
{
    public Material shadowMaterial;

    void OnRenderImage(RenderTexture source, RenderTexture destination){
        Graphics.Blit(source, destination, shadowMaterial);


    }
}
