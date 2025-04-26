using TMPro;
using UnityEngine;

public class StarDisplay : MonoBehaviour
{
    private TextMeshPro textMesh;


    void Start()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    void Update()
    {
        if (StarManager.Instance != null)
        {
            textMesh.text = "" + StarManager.Instance.GetStarCount().ToString();
        }
    }
}
