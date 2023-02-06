using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HueText : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;
    [SerializeField]
    private float saturation = 1.0f, brightness = 1.0f;

    [SerializeField]
    private TextMeshProUGUI textMesh;

    void Start()
    {
        textMesh.outlineWidth = 0.2f;
    }

    void Update()
    {
        float time = Time.time * speed;

        VertexGradient gradient = new VertexGradient();
        gradient.topLeft = Color.HSVToRGB( time % 1.0f, saturation, brightness);
        gradient.bottomLeft = gradient.topLeft;
        gradient.topRight = Color.HSVToRGB( ( time - 0.1f ) % 1.0f, saturation, brightness);
        gradient.bottomRight = gradient.topRight;
        textMesh.colorGradient = gradient;
    }
}
