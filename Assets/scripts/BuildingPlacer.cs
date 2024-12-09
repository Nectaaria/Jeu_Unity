using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class BuildingPlacer : MonoBehaviour
{
    public static BuildingPlacer instance; // (Singleton pattern)

    public LayerMask groundLayerMask;

    protected GameObject buildingPrefab;
    protected GameObject toBuild;

    protected Camera mainCamera;

    protected RaycastHit hit;

    private void Awake()
    {
        instance = this; // (Singleton pattern)
        mainCamera = Camera.main;
        buildingPrefab = null;
    }

    private void OnMousePosition(InputValue value)
    {

    }
    private void Update()
    {
        if (buildingPrefab != null)
        { // if in build mode

            // right-click: cancel build mode
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(toBuild);
                toBuild = null;
                buildingPrefab = null;
                return;
            }

            // hide preview when hovering UI
            if (EventSystem.current.IsPointerOverGameObject())
            {
                if (toBuild.activeSelf) toBuild.SetActive(false);
                return;
            }
            else if (!toBuild.activeSelf) toBuild.SetActive(true);

            // rotate preview with Spacebar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                toBuild.transform.Rotate(Vector3.up, 90);
            }

            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out hit, 1000f, groundLayerMask))
            {
                if (!toBuild.activeSelf) toBuild.SetActive(true);
                toBuild.transform.position = hit.point;

                if (Input.GetMouseButtonDown(0))
                { // if left-click
                    BuildingManager m = toBuild.GetComponent<BuildingManager>();
                    if (m.hasValidPlacement)
                    {
                        m.SetPlacementMode(PlacementMode.Fixed);

                        // shift-key: chain builds
                        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                        {
                            toBuild = null; // (to avoid destruction)
                            PrepareBuilding();
                        }
                        // exit build mode
                        else
                        {
                            buildingPrefab = null;
                            toBuild = null;
                        }
                    }
                }

            }
            else if (toBuild.activeSelf) toBuild.SetActive(false);
        }
    }

    public void SetBuildingPrefab(GameObject prefab)
    {
        buildingPrefab = prefab;
        PrepareBuilding();
        EventSystem.current.SetSelectedGameObject(null); // cancel keyboard UI nav
    }

    protected virtual void PrepareBuilding()
    {
        if (toBuild) Destroy(toBuild);

        toBuild = Instantiate(buildingPrefab);
        toBuild.SetActive(false);

        BuildingManager m = toBuild.GetComponent<BuildingManager>();
        m.isFixed = false;
        m.SetPlacementMode(PlacementMode.Valid);
    }

}