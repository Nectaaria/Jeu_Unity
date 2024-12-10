
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlacementMode
{
    Fixed,
    Valid,
    Invalid
}

public class BuildingManager : MonoBehaviour
{
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;

    [SerializeField] private int woodRequired;
    [SerializeField] private int stoneRequired;
    [SerializeField] private GameObject gameManager;
    private Dictionary<string, int> inventory;


    public MeshRenderer[] meshComponents;
    private Dictionary<MeshRenderer, List<Material>> initialMaterials;

    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;

    private int nObstacles;

    private void Awake()
    {
        hasValidPlacement = true;
        isFixed = true;
        nObstacles = 0;

        InitializeMaterials();
    }
    private void Start()
    {
        Game gameScript = gameManager.GetComponent<Game>();
        inventory = gameScript.inventory;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFixed) return;

        // ignore ground objects
        if (IsGround(other.gameObject)) return;

        nObstacles++;
        SetPlacementMode(PlacementMode.Invalid);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isFixed) return;

        // ignore ground objects
        if (IsGround(other.gameObject)) return;

        nObstacles--;
        if (nObstacles == 0)
            SetPlacementMode(PlacementMode.Valid);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        InitializeMaterials();
    }
#endif

    public void SetPlacementMode(PlacementMode mode)
    {
        Game gameScript = gameManager.GetComponent<Game>();
        inventory = gameScript.inventory;
        if (mode == PlacementMode.Fixed)
        {
            isFixed = true;
            hasValidPlacement = true;
        }
        else if (mode == PlacementMode.Valid && inventory["wood"] >= woodRequired && inventory["stone"] >= stoneRequired)
        {
            hasValidPlacement = true;
        }
        else
        {
            hasValidPlacement = false;
        }
        SetMaterial(mode);
    }

    public void SetMaterial(PlacementMode mode)
    {
        if (mode == PlacementMode.Fixed)
        {
            foreach (MeshRenderer r in meshComponents)
                r.sharedMaterials = initialMaterials[r].ToArray();
        }
        else
        {
            Material matToApply = mode == PlacementMode.Valid
                ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] m; int nMaterials;
            foreach (MeshRenderer r in meshComponents)
            {
                nMaterials = initialMaterials[r].Count;
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;
                r.sharedMaterials = m;
            }
        }
    }

    private void InitializeMaterials()
    {
        if (initialMaterials == null)
            initialMaterials = new Dictionary<MeshRenderer, List<Material>>();
        if (initialMaterials.Count > 0)
        {
            foreach (var l in initialMaterials) l.Value.Clear();
            initialMaterials.Clear();
        }

        foreach (MeshRenderer r in meshComponents)
        {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);
        }
    }

    private bool IsGround(GameObject o)
    {
        return ((1 << o.layer) & BuildingPlacer.instance.groundLayerMask.value) != 0;
    }

}
