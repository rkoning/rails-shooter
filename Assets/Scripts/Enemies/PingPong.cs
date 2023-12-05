using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PingPongAxis {
    X,
    Y,
    Z
}

public enum PingPongOscillationFunction {
    Sin,
    Cos
}

public class PingPong : MotionFunction
{
    public float Frequency = 2f;
    public float Range = 10f;
    public float OffsetScaling = .5f;
    public PingPongOscillationFunction Function = PingPongOscillationFunction.Sin;
    public PingPongAxis Axis = PingPongAxis.X;
    
    public override Vector3 GetPositionAtTime(float time, int offset)
    {
        Func<float, float> oscillationFunc = GetOscillationFunction(Function);

        float value = Frequency * time + offset * OffsetScaling * Mathf.PI;
        return Axis switch
        {
            PingPongAxis.X => Range * new Vector3(oscillationFunc(value), 0f, 0f),
            PingPongAxis.Y => Range * new Vector3(0f, oscillationFunc(value), 0f),
            PingPongAxis.Z => Range * new Vector3(0f, 0f, oscillationFunc(value)),
            _ => Range * new Vector3(oscillationFunc(value), 0f, 0f),
        };
    }

    private Func<float, float> GetOscillationFunction(PingPongOscillationFunction fcn) {
        return fcn switch {
            PingPongOscillationFunction.Sin => Mathf.Sin,
            PingPongOscillationFunction.Cos => Mathf.Cos,
            _ => Mathf.Sin,
        };
    }
}
