using System;
#if UNITY_EDITOR
using System.Linq;
#endif
using UnityEngine;
using System.Collections;

public class TerrainRegion : MonoBehaviour
{
    public int Length;
    [SerializeField]
    private Renderer terrainPartPrefab;

	// Use this for initialization
	void Start () {
	
	}

    [ContextMenu("Rebuild")]
    void Rebuild()
    {
#if UNITY_EDITOR
        removeAllContent();

        float x = 0;
        for (int i = 0; i < Length; i++)
        {
            Renderer child = (Renderer) Instantiate(terrainPartPrefab);
            Append(child);

            child.name = "TerrainPart" + i;
            Vector3 leftShift = child.transform.position - child.bounds.min;
            Vector3 rightShift = child.bounds.max - child.transform.position;
            x += child.transform.InverseTransformVector(leftShift).x;
            child.transform.localPosition = new Vector3(x, 0 ,0);
            x += child.transform.InverseTransformVector(rightShift).x;
        }
#endif
    }

    private void Append(Renderer child)
    {
        child.transform.parent = transform;
        child.transform.localScale = Vector3.one;
        child.transform.tag = transform.tag;
        child.gameObject.layer = gameObject.layer;
    }

    private void removeAllContent()
    {
#if UNITY_EDITOR
        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }
#else
        foreach (Transform trans in transform)
        {

            GameObject.Destroy(trans.gameObject);
        }
#endif
    }
}
