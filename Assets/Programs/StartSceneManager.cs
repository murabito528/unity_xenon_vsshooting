using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public int phase;//0:press key 1:select play 

    [SerializeField]
    GameObject pressstart;

    // Start is called before the first frame update
    void Start()
    {
        phase = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case 0:
                if (Input.anyKey && !Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
                {
                    pressstart.GetComponent<BlinkController>().pressed();
                    phase = 1;
                }
                break;
        }
    }
}
