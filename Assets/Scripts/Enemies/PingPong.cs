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
    public float frequency = 2f;
    public float range = 10f;
    public float offsetScaling = .5f;
    public PingPongOscillationFunction function = PingPongOscillationFunction.Sin;
    public PingPongAxis axis = PingPongAxis.X;
    
    public override Vector3 GetPositionAtTime(float time, int offset)
    {
        Func<float, float> oscillationFunc = GetOscillationFunction(function);

        float value = frequency * time + offset * offsetScaling * Mathf.PI;
        return axis switch
        {
            PingPongAxis.X => range * new Vector3(oscillationFunc(value), 0f, 0f),
            PingPongAxis.Y => range * new Vector3(0f, oscillationFunc(value), 0f),
            PingPongAxis.Z => range * new Vector3(0f, 0f, oscillationFunc(value)),
            _ => range * new Vector3(oscillationFunc(value), 0f, 0f),
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
