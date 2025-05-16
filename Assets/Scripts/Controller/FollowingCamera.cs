using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    public float distanceAway = 7.0f; //�� ����
    public float distanceUp = 1.0f;   //y��

    public Transform target; //�÷��̾�

    private void LateUpdate()
    {
        //ī�޶� ��ġ : Ÿ�� ��ġ + �� - ��

        if (target != null)
        {
           transform.position = target.position + Vector3.up * distanceUp - Vector3.forward * distanceAway;
        }
    }
}
