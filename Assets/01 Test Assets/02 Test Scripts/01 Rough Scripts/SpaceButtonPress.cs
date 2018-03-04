using UnityEngine;
using System.Collections;

public class SpaceButtonPress : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            print("YES MY MAN");
    }
}