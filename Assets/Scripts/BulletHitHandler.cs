using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitHandler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hitEffectReference;

    private static ParticleSystem hitEfect;
    private static Transform hitEfectTransform;

    private void Start()
    {
        hitEfect = hitEffectReference;
        hitEfectTransform = hitEfect.transform;
    }

    public static void EmitFromPoint(Vector3 pos, Quaternion rot)
    {
        hitEfectTransform.position = pos;
        hitEfectTransform.rotation = rot;
        hitEfect.Emit(Random.Range(6,10));
    }
}
