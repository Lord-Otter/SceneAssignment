using System.Collections.Generic;
using UnityEngine;

public class TriggerMeshToggle : MonoBehaviour
{
    public List<GameObject> objectsToToggle = new List<GameObject>();

    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    private List<SkinnedMeshRenderer> skinnedMeshRenderers = new List<SkinnedMeshRenderer>();
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    private int playerTriggerCount = 0;

    void Start()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj == null) continue;

            MeshRenderer[] mrs = obj.GetComponentsInChildren<MeshRenderer>(true);
            SkinnedMeshRenderer[] skmrs = obj.GetComponentsInChildren<SkinnedMeshRenderer>(true);
            SpriteRenderer[] srs = obj.GetComponentsInChildren<SpriteRenderer>(true);

            meshRenderers.AddRange(mrs);
            skinnedMeshRenderers.AddRange(skmrs);
            spriteRenderers.AddRange(srs);

            foreach (SpriteRenderer sr in srs)
                sr.enabled = false;
        }
    }

    void Update()
    {
        bool playerInside = playerTriggerCount > 0;

        if (playerInside)
            SetRenderState(true);
        else
            SetRenderState(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerTriggerCount++;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        playerTriggerCount = Mathf.Max(0, playerTriggerCount - 1);
    }

    void SetRenderState(bool meshState)
    {
        foreach (MeshRenderer mr in meshRenderers)
        {
            if (mr != null)
                mr.enabled = meshState;
        }

        foreach(SkinnedMeshRenderer skmr in skinnedMeshRenderers)
        {
            if(skmr != null)
                skmr.enabled = meshState;
        }

        foreach (SpriteRenderer sr in spriteRenderers)
        {
            if (sr != null)
                sr.enabled = !meshState;
        }
    }
}
