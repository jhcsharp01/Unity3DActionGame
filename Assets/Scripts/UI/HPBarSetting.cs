using UnityEngine;

public class HPBarSetting : MonoBehaviour
{
    private Quaternion Quaternion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Quaternion = transform.localRotation;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localRotation = Quaternion;
    }
}
