using UnityEngine;

public class DistanceRendererCuller : MonoBehaviour
{
    public Transform player;
    public float disableDistance = 20f;

    private MeshRenderer[] meshRenderers;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        InvokeRepeating(nameof(CheckDistance), 0f, 0.5f);
    }

    void CheckDistance()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool shouldRender = distance <= disableDistance;

        foreach (MeshRenderer mr in meshRenderers)
            mr.enabled = shouldRender;
    }
}
