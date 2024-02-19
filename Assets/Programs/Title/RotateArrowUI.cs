using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateArrowUI : MonoBehaviour
{
    [SerializeField]
    float speed;

    RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.rotation = Quaternion.Euler(rt.rotation.eulerAngles + Utilities.vec.forward * Time.deltaTime * speed);
    }
}
