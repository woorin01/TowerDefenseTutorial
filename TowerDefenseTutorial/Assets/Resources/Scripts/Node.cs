using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffSet;
    public GameObject tower;

    private MeshRenderer meshRend;
    private Color startColor;

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

        if(tower != null)
        {
            Debug.Log("The turret is already installed");
            return;
        }

        BuildManager.instance.BuildTowerOn(this);
        meshRend.material.color = startColor;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!BuildManager.instance.CanBuild)
            return;

        if (tower == null)
            meshRend.GetComponent<MeshRenderer>().material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        meshRend.material.color = startColor;    
    }
}
