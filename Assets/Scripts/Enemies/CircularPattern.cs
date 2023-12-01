using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularPattern : MonoBehaviour
{
    public List<Transform> objects;
    public float radius;
    public float speed;

    public bool evenSpacing;
    public float spacing = 4f;
    public float spacingOffset = 0f;

    public float zPingPongMul = 2f;
    private void Start() {
        if (evenSpacing) {
            spacing = objects.Count;
        }
    }

    private void Update() {
        float now = Time.fixedTime;
        for (int i = 0; i < objects.Count; i++) {
            var o = objects[i];
            if (o == null)
                continue;

            o.transform.position = transform.position + OrbitXY(radius, now, i);
            // + PingPongZ(radius, now * i * zPingPongMul, i);
        }
    }

    private Vector3 OrbitXY(float radius, float now, int index) {
        float oTime = now * speed + ((index + 1) * Mathf.PI / spacing + spacingOffset * Mathf.PI);
        return radius * new Vector3(Mathf.Sin(oTime), Mathf.Cos(oTime), 0f);
    }

    private Vector3 PingPongZ(float radius, float now, int index) {
        float oTime = now * speed + ((index + 1) * Mathf.PI / spacing + spacingOffset * Mathf.PI);
        return radius * new Vector3(0f, 0f, Mathf.Sin(oTime));
    }
}
