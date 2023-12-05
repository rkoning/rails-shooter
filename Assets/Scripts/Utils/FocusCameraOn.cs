using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCameraOn : MonoBehaviour
{

    public List<Transform> pointsOfInterest;
    public Vector3 offset = new Vector3(0, 0, -10);
    private int current;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            current = WrapIndex(current + 1);
            transform.position = pointsOfInterest[current].position + offset;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            current = WrapIndex(current - 1);
            transform.position = pointsOfInterest[current].position + offset;
        }
    }

    private int WrapIndex(int index) {
        if (index >= pointsOfInterest.Count) {
            return 0;
        } else if (index < 0) {
            return pointsOfInterest.Count - 1;
        }
        return index;
    }
}
