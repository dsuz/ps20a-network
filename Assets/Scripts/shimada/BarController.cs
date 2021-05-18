using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarController : MonoBehaviour
{
    [SerializeField] bool m_rotate = false;
    // Update is called once per frame
    void Update()
    {
        if (m_rotate)
        {
            this.gameObject.transform.Rotate(new Vector3(0, 0, 1));
        }
        else
        {

        }
    }
}
