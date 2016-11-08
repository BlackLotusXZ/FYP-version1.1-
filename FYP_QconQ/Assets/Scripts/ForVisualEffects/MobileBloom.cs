using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Mobile Bloom V2")]

public class MobileBloom : MonoBehaviour
{

    public float intensity = 0.7f;
    public float threshhold = 0.75f;
    public float blurWidth = 1.0f;

    public bool extraBlurry = false;

    // image effects materials for internal use

    public Material bloomMaterial = null;

    private bool supported = false;

    private RenderTexture tempRtA = null;
    private RenderTexture tempRtB = null;

    public float newIntensity = 0.0f; 

    void Start()
    {
        newIntensity = intensity;
        StartCoroutine(intensityLerp());
    }

    bool Supported()
    {
        if (supported) return true;
        supported = (SystemInfo.supportsImageEffects && SystemInfo.supportsRenderTextures && bloomMaterial.shader.isSupported);
        return supported;
    }

    void CreateBuffers()
    {
        if (!tempRtA)
        {
            tempRtA = new RenderTexture(Screen.width / 4, Screen.height / 4, 0);
            tempRtA.hideFlags = HideFlags.DontSave;
        }
        if (!tempRtB)
        {
            tempRtB = new RenderTexture(Screen.width / 4, Screen.height / 4, 0);
            tempRtB.hideFlags = HideFlags.DontSave;
        }
    }

    void OnDisable()
    {
        if (tempRtA)
        {
            DestroyImmediate(tempRtA);
            tempRtA = null;
        }
        if (tempRtB)
        {
            DestroyImmediate(tempRtB);
            tempRtB = null;
        }
    }

    bool EarlyOutIfNotSupported(RenderTexture source, RenderTexture destination)
    {
        if (!Supported())
        {
            enabled = false;
            Graphics.Blit(source, destination);
            return true;
        }
        return false;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        CreateBuffers();
        if (EarlyOutIfNotSupported(source, destination))
            return;

        // prepare data

        bloomMaterial.SetVector("_Parameter", new Vector4(0.0f, 0.0f, threshhold, intensity / (1.0f - threshhold)));

        // ds & blur

        float oneOverW = 1.0f / (source.width * 1.0f);
        float oneOverH = 1.0f / (source.height * 1.0f);

        bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * oneOverW, 1.5f * oneOverH, -1.5f * oneOverW, 1.5f * oneOverH));
        bloomMaterial.SetVector("_OffsetsB", new Vector4(-1.5f * oneOverW, -1.5f * oneOverH, 1.5f * oneOverW, -1.5f * oneOverH));

        Graphics.Blit(source, tempRtB, bloomMaterial, 1);

        oneOverW *= 4.0f * blurWidth;
        oneOverH *= 4.0f * blurWidth;

        bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * oneOverW, 0.0f, -1.5f * oneOverW, 0.0f));
        bloomMaterial.SetVector("_OffsetsB", new Vector4(0.5f * oneOverW, 0.0f, -0.5f * oneOverW, 0.0f));
        Graphics.Blit(tempRtB, tempRtA, bloomMaterial, 2);

        bloomMaterial.SetVector("_OffsetsA", new Vector4(0.0f, 1.5f * oneOverH, 0.0f, -1.5f * oneOverH));
        bloomMaterial.SetVector("_OffsetsB", new Vector4(0.0f, 0.5f * oneOverH, 0.0f, -0.5f * oneOverH));
        Graphics.Blit(tempRtA, tempRtB, bloomMaterial, 2);

        if (extraBlurry)
        {
            bloomMaterial.SetVector("_OffsetsA", new Vector4(1.5f * oneOverW, 0.0f, -1.5f * oneOverW, 0.0f));
            bloomMaterial.SetVector("_OffsetsB", new Vector4(0.5f * oneOverW, 0.0f, -0.5f * oneOverW, 0.0f));
            Graphics.Blit(tempRtB, tempRtA, bloomMaterial, 2);

            bloomMaterial.SetVector("_OffsetsA", new Vector4(0.0f, 1.5f * oneOverH, 0.0f, -1.5f * oneOverH));
            bloomMaterial.SetVector("_OffsetsB", new Vector4(0.0f, 0.5f * oneOverH, 0.0f, -0.5f * oneOverH));
            Graphics.Blit(tempRtA, tempRtB, bloomMaterial, 2);
        }

        // bloomMaterial

        bloomMaterial.SetTexture("_Bloom", tempRtB);
        Graphics.Blit(source, destination, bloomMaterial, 0);
    }

    IEnumerator intensityLerp()
    {
        while(true)
        {
            intensity = Mathf.Lerp(intensity, newIntensity, Time.deltaTime * 5.0f);
            yield return null;
        }
    }
}
