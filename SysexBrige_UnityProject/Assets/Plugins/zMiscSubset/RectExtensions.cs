using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class RectExtensions
{

    public static void removeChildren(this Transform transform, int childIndex = 0)
    {
        for (int i = transform.childCount - 1; i > childIndex; i--)
        {

            GameObject go = transform.GetChild(i).gameObject;
            MonoBehaviour.Destroy(go);
        }
    }
    public static RectTransform AddChild(this RectTransform parentRect)
    {
        GameObject go = new GameObject();
        RectTransform rect = go.getRect();


        go.transform.SetParent(parentRect);

        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(1, 1);
        rect.sizeDelta = new Vector2(10, 10);
        rect.offsetMin = new Vector2(5, 5);
        rect.offsetMax = new Vector2(-5, -5);
        rect.localPosition = Vector2.zero;
//Debug.Log(" added child to ",parentRect.gameObject);
//	Debug.Log("new object is",rect.gameObject);

        return rect;
    }


    public static RectTransform AddChild(this GameObject parent)
    {  RectTransform parentRect=parent.GetComponent<RectTransform>();
               return parentRect.AddChild();
    }
    public static void LayoutParameters(this VerticalLayoutGroup vl)
    {
        vl.spacing = 2;
        vl.padding = new RectOffset(3, 3, 3, 3);
        vl.childControlWidth = true;
        vl.childControlHeight = true;
        vl.childForceExpandHeight = false;
        vl.childForceExpandWidth = false;
    }
    public static void setRelativeSizeX(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.sizeDelta = new Vector2(sizeX * v, rect.sizeDelta.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeSizeY(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeY = parentRect.rect.height;
        if (v.checkFloat())
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, sizeY * v);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeLocalY(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeY = parentRect.rect.height;
        if (v.checkFloat())
            rect.localPosition = new Vector2(rect.localPosition.x, sizeY * v);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setSizeXY(this RectTransform rect, float x, float y)
    {

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, y);

    }
    public static void setSizeX(this RectTransform rect, float v)
    {

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, v);

    }
    public static void setSizeY(this RectTransform rect, float v)
    {

        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, v);

    }
    public static float getWidth(this RectTransform rect)
    {

        return rect.rect.width;

    }

    public static float getHeight(this RectTransform rect)
    {

        return rect.rect.height;

    }
    public static void setLocalX(this RectTransform rect, float v)
    {
        if (float.IsNaN(v)) return;
        if (rect == null) return;
        rect.localPosition = new Vector2(v, rect.localPosition.y);

    }
    public static void setLocalY(this RectTransform rect, float v)
    {
        rect.localPosition = new Vector2(rect.localPosition.y, v);

    }
    public static void setRelativeLocalX(this RectTransform rect, RectTransform parentRect, float v)
    {
        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.localPosition = new Vector2(sizeX * v, rect.localPosition.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);

    }

    public static void setRelativeStartX(this RectTransform rect, RectTransform parentRect, float v)
    {

        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.offsetMin = new Vector2(sizeX * v, rect.offsetMin.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeEndX(this RectTransform rect, RectTransform parentRect, float v)
    {

        float sizeX = parentRect.rect.width;
        if (v.checkFloat())
            rect.offsetMax = new Vector2(-sizeX * v, rect.offsetMax.y);
        else Debug.Log("source is" + rect.name + " parent " + parentRect.name, rect.gameObject);
    }
    public static void setRelativeStartX(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        rect.setRelativeStartX(parentRect, v);

    }
    public static void setRelativeEndX(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        rect.setRelativeEndX(parentRect, v);
    }
    public static void setRelativeEndY(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        float sizeY = parentRect.rect.height;
        rect.offsetMin = new Vector2(rect.offsetMin.x, sizeY * v);
    }
    public static void setRelativeStartY(this RectTransform rect, float v)
    {
        if (rect.transform.parent == null)
        {
            Debug.Log("no parent", rect);
            return;
        }
        RectTransform parentRect = rect.transform.parent.GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.Log("no parent RectTransform Component", rect);
            return;
        }
        float sizeY = parentRect.rect.height;
        rect.offsetMax = new Vector2(rect.offsetMax.x, -sizeY * v);

    }
    public static void setAnchorLeft(this RectTransform rect, float v)
    {
        rect.anchorMin = new Vector2(v, rect.anchorMin.y);
    }
    public static void setAnchorRight(this RectTransform rect, float v)
    {
        rect.anchorMax = new Vector2(1 - v, rect.anchorMax.y);
    }
    public static void setAnchorTop(this RectTransform rect, float v)
    {
        rect.anchorMax = new Vector2(rect.anchorMax.x, v);
    }
    public static void setAnchorBottom(this RectTransform rect, float v)
    {
        rect.anchorMin = new Vector2(rect.anchorMin.x, v);
    }
    public static void setAnchorsY(this RectTransform rect, float min, float max)
    {
        rect.anchorMin = new Vector2(min, rect.anchorMin.y);
        rect.anchorMax = new Vector2(max, rect.anchorMax.y);
    }
    public static void setAnchorsX(this RectTransform rect, float min, float max)
    {
        rect.anchorMin = new Vector2(rect.anchorMin.x, min);
        rect.anchorMax = new Vector2(rect.anchorMax.y, max);
    }
    public static void setAnchorX(this RectTransform rect, float v)
    {
        rect.anchorMin = new Vector2(v, rect.anchorMin.y);
        rect.anchorMax = new Vector2(v, rect.anchorMax.y);
    }
    public static void setPivotX(this RectTransform rect, float v)
    {
        float deltaPivot = rect.pivot.x - v;
        Vector2 temp = rect.localPosition;
        rect.pivot = new Vector2(v, rect.pivot.y);
        rect.localPosition = temp - new Vector2(deltaPivot * rect.rect.width * rect.localScale.x, 0);
    }
    public static void setPivotY(this RectTransform rect, float v)
    {
        float deltaPivot = rect.pivot.y - v;
        Vector2 temp = rect.localPosition;
        rect.pivot = new Vector2(rect.pivot.x, v);
        rect.localPosition = temp - new Vector2(0, deltaPivot * rect.rect.height * rect.localScale.y);
    }
    public static void setPivot(this RectTransform rect, float x, float y)
    {
        float deltaPivotx = rect.pivot.x - x;
        float deltaPivoty = rect.pivot.y - y;
        Vector2 temp = rect.localPosition;
        rect.pivot = new Vector2(x, y);
        rect.localPosition = temp - new Vector2(deltaPivotx * rect.rect.width * rect.localScale.x, deltaPivoty * rect.rect.height * rect.localScale.y);
    }

    public static void setTopLeftAnchor(this RectTransform rect, Vector2 newAnchor)
    {
        Vector2 temp = rect.sizeDelta;
        rect.anchorMin = new Vector2(newAnchor.x, rect.anchorMin.y);
        rect.anchorMax = new Vector2(rect.anchorMax.x, newAnchor.y);
        rect.sizeDelta = temp;

    }
    public static void setBottomRightAnchor(this RectTransform rect, Vector2 newAnchor)
    {
        rect.anchorMin = new Vector2(rect.anchorMin.x, newAnchor.y);
        rect.anchorMax = new Vector2(newAnchor.x, rect.anchorMax.y);

    }

    public static RectTransform getRect(this GameObject go)
    {
        RectTransform rect = go.GetComponent<RectTransform>();
        if (rect == null) rect = go.AddComponent<RectTransform>();
        return rect;

    }

    /// <summary>
    /// Creates a child recttransorm
    /// </summary>

    public static RectTransform getChild(this RectTransform thisRect)
    {

        RectTransform rect = thisRect.GetComponent<RectTransform>();
        if (rect == null) rect = thisRect.gameObject.AddComponent<RectTransform>();
        return rect;

    }
    /// <summary>
    /// Gets children, if True gets all children of children as well
    /// </summary>

    public static GameObject[] getChildren(this GameObject thisGo, bool deep = false)
    {
        if (!deep)
        {
            Transform t = thisGo.transform;
            GameObject[] c = new GameObject[t.childCount];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = t.GetChild(i).gameObject;
            }
            return c;
        }
        else
        {
            Transform[] transforms = thisGo.GetComponentsInChildren<Transform>(true);
            GameObject[] c = new GameObject[transforms.Length - 1];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = transforms[i + 1].gameObject;
            }
            return c;
        }
    }

    /// <summary>
    /// gets children for an array, useful for editor selections 
    /// </summary>

    public static GameObject[] getChildrenArray(this GameObject[] thisGoArray, bool deep = false)
    {
        List<GameObject> children = new List<GameObject>();
        for (int i = 0; i < thisGoArray.Length; i++)
            children.AddRange((thisGoArray[i]).getChildren(deep));


        return children.ToArray();
    }


    public static GameObject[] getAllChildrenCalled(this GameObject[] thisGoArray, string name)
    {
        GameObject[] children = thisGoArray.getChildrenArray(true);
        List<GameObject> namedObjects = new List<GameObject>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i].name.Equals(name)) namedObjects.Add(children[i]);
        }
        return namedObjects.ToArray();


    }
}
