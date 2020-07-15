using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{

    public Transform alvo;
    public Vector3 posicaoFinal;
    public Vector3 Offset;

    void LateUpdate()
    {
        posicaoFinal = alvo.position;

        transform.position = posicaoFinal + Offset;
    }
}
