using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ParameterPack
{
    public int[] Parameters;

    public int ColonistsAlive;
    public int TechLevel;
    public int CultureLevel;
    public int MoraleLevel;

    public ParameterPack(
        int parameter1,
        int parameter2,
        int parameter3,
        int parameter4,
        int colonistsAlive,
        int techLevel,
        int cultureLevel,
        int moraleLevel)
    {
        Parameters = new int[4];
        Parameters[0] = parameter1;
        Parameters[1] = parameter2;
        Parameters[2] = parameter3;
        Parameters[3] = parameter4;
        this.ColonistsAlive = colonistsAlive;
        this.TechLevel = techLevel;
        this.CultureLevel = cultureLevel;
        this.MoraleLevel = moraleLevel;

    }
}
