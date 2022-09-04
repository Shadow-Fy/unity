using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTips : MonoBehaviour
{
    public Text itemname;
    public Text iteminfo;
    RectTransform recttransform;

    void Awake()
    {
        recttransform = GetComponent<RectTransform>();
    }

    public void SetupTooltip(ItemData_SO item)
    {
        itemname.text = item.itemname;
        iteminfo.text = item.description;
    }

    void OnEnable()
    {
        UpdatePosition();
    }

    void Update()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        Vector3 mousepos = Input.mousePosition;
        Vector3[] corners = new Vector3[4];
        recttransform.GetWorldCorners(corners);

        float width = corners[3].x - corners[0].x;
        float height = corners[1].y - corners[0].y;

        if (mousepos.y < height)
        {
            recttransform.position = mousepos + Vector3.up * height * 0.6f;
        }
        else if (Screen.width - mousepos.x > width)
        {
            recttransform.position = mousepos + Vector3.right * width * 0.6f;
        }
        else
            recttransform.position = mousepos + Vector3.left * width * 0.6f;

    }
}
