using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color startColor;
    public Color hoverColor;
    public Vector3 positionOffSet;
    public GameObject tower;

    private MeshRenderer meshRend;
    public MeshRenderer MeshRend
    {
        get
        {
            return meshRend;
        }
        set
        {
            meshRend = value;
        }
    }//get set

    private void Start()
    {
        meshRend = GetComponent<MeshRenderer>();
        startColor = meshRend.material.color;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffSet;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.instance.CanBuild)
            return;

        if (tower != null)
        {
            Debug.Log("The turret is already installed");
            return;
        }

        BuildManager.instance.BuildTowerOn(this);
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.instance.CanBuild)
            return;

        if (BuildManager.instance.HasMoney && tower == null)
            meshRend.material.color = hoverColor;
        else
            meshRend.material.color = Color.red;

    }

    private void OnMouseExit()
    {
        meshRend.material.color = startColor;
    }
}
