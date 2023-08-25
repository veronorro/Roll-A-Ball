using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public GameObject particlePrefab;

    public void CreatePartciles()
    {
        Instantiate(particlePrefab, transform.localPosition, transform.localRotation);
    }
}
