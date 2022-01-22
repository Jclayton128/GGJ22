using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Outcome 
{
    public ParameterTracker.Parameter Parameter;
    public int Magnitude;

    public Outcome(ParameterTracker.Parameter newParameter, int magnitude)
    {
        this.Parameter = newParameter;
        this.Magnitude = magnitude;
    }
}
