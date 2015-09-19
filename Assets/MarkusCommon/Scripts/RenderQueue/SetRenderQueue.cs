using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Set render queue of current game obejct.
/// </summary>
/// <remarks>
/// Useful in unmanagement parts of NGUI, like particle system.
/// This may cause material leak. Please look into it.
/// </remarks>
public class SetRenderQueue : MonoBehaviour
{
    public int RenderQueue = 4000;

    private List<ParticleSystem> particleSystemList;
    private List<MeshRenderer> meshRendererList;
    private List<SkinnedMeshRenderer> skinnedMeshRendererList;
    private Material material;

    public void SetQueue()
    {
        foreach (var psSystem in particleSystemList)
        {
            var ren = psSystem.gameObject.renderer ?? psSystem.renderer;
            ReplaceMaterial(ren);
        }

        foreach (var meshRenderer in meshRendererList)
        {
            ReplaceMaterial(meshRenderer);
        }

        foreach (var meshRenderer in skinnedMeshRendererList)
        {
            ReplaceMaterial(meshRenderer);
        }
    }

    private void ReplaceMaterial(Renderer ren)
    {
        if (ren != null)
        {
            if (ren.sharedMaterial == null)
            {
                Logger.LogError("ren.sharedMaterial is null");
                return;
            }

            material = new Material(ren.sharedMaterial) { renderQueue = RenderQueue };
            ren.material = material;
        }
    }

    void Start()
    {
        particleSystemList = new List<ParticleSystem>();
        particleSystemList.AddRange(GetComponentsInChildren<ParticleSystem>());
        meshRendererList = new List<MeshRenderer>();
        meshRendererList.AddRange(GetComponentsInChildren<MeshRenderer>());
        skinnedMeshRendererList = new List<SkinnedMeshRenderer>();
        skinnedMeshRendererList.AddRange(GetComponentsInChildren<SkinnedMeshRenderer>());

        SetQueue();
    }

    void OnDestroy()
    {
        if (material != null) Destroy(material);
    }
}