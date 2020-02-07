using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyMatter : MonoBehaviour
{
    public int verticeIndex;
    public Vector3 initialVertexPos;
    public Vector3 currentVertexPos;

    public Vector3 currentVelocity;

    public JellyMatter(int _verticeIndex, Vector3 _initialVertexPos, Vector3 _currentVertexPos, Vector3 _currentVelocity)
    {
        verticeIndex = _verticeIndex;
        initialVertexPos = _initialVertexPos;
        currentVertexPos = _currentVertexPos;
        currentVelocity = _currentVelocity;
    }

    public Vector3 GetCurrentDisplacement()
    {
        return currentVertexPos - initialVertexPos;
    }

    public void UpdateVelocity(float _bounceSpeed)
    {
        currentVelocity = currentVelocity - GetCurrentDisplacement() * _bounceSpeed * Time.deltaTime;
    }

    public void Settle(float _stiffness)
    {
        currentVelocity *= 1f - _stiffness * Time.deltaTime;
    }

    public void ApplyPressureToVertex()
    {

    }
}
