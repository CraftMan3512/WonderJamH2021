using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TiltCam : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    float spin = 0;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Sanity < 70)
        {
            cam.m_Lens.Dutch += Time.deltaTime * 0.5f * (100f / (GameManager.Sanity + 1))*Mathf.Sign(spin);
        }
        else
        {
            cam.m_Lens.Dutch = 0;
        }
        if(Mathf.Abs(spin) <= 0.2f)
        {
            spin = Random.Range(-5f, 5f);
        }
        else
        {
            spin -= Time.deltaTime * Mathf.Sign(spin);
        }
    }
}
