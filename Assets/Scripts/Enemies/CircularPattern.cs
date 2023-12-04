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

    public float formationRadius = 8f;
    public float formationSpeed = .5f;

    private void Start() {
        if (evenSpacing) {
            spacing = objects.Count;
        }
    }

    private void Update() {
        float now = Time.fixedTime;
        Vector3 refPoint = OrbitXY(formationRadius, now, 0, formationSpeed);
        for (int i = 0; i < objects.Count; i++) {
            var o = objects[i];
            if (o == null)
                continue;

            o.transform.position = transform.position + refPoint + OrbitXY(radius, now, i, speed);
            // + PingPongZ(radius, now * i * zPingPongMul, i);
        }
    }

    private Vector3 OrbitXY(float radius, float now, int offset, float speed) {
        float oTime = now * speed + ((offset + 1) * Mathf.PI / spacing + spacingOffset * Mathf.PI);
        return radius * new Vector3(Mathf.Sin(oTime), Mathf.Cos(oTime), 0f);
    }

    private Vector3 PingPongZ(float radius, float now, int index, float speed) {
        float oTime = now * speed + ((index + 1) * Mathf.PI / spacing + spacingOffset * Mathf.PI);
        return radius * new Vector3(0f, 0f, Mathf.Sin(oTime));
    }
}
