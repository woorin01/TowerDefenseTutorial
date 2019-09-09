using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeUI : MonoBehaviour
{
    public GameObject canvas;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = _target.GetBuildPosition();

        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }
}
