using System;
using System.Collections.Generic;
using System.Linq;

public class NoiseFilter
{
    private float value;
    private const int MinData = 10;
    private const int MaxData = 25;
    private List<float> raw_values = new List<float>();

    public NoiseFilter()
    {
    }

    public float GetValue()
    {
        return raw_values.Average();
    }

    public void Process(float rawValue)
    {
        raw_values.Add(rawValue);
        if (raw_values.Count > MaxData)
        {
            raw_values.RemoveAt(0);
        }

        return;
    }
}