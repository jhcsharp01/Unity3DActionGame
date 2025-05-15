using System;
using UnityEngine;

public class TouchPad : MonoBehaviour
{
    private RectTransform _touchPad; //UI ��ġ�� RectTransform

    private int _touchId = -1; //���� ��Ʈ�ѷ� ���� �ȿ� �ִ� �Է� ���п� ���̵�

    private Vector3 _startPos = Vector3.zero; //�Է� ���� ��ǥ

    public float _dragRadius = 60.0f; //���� ��Ʈ�ѷ� �� �����ӿ� ������

    public PlayerMovement _player; //����Ű ���濡 ���� ĳ���Ϳ��� ����

    private bool _buttonPressed = false; //��ư ���� ����

    private void Start()
    {
        //������Ʈ ����
        _touchPad = GetComponent<RectTransform>();
        //��ġ �е� ��ǥ (���� ��)
        _startPos = _touchPad.position;
    }

    public void ButtonDown()
    {
        _buttonPressed = true;
    }
    public void ButtonUp() => _buttonPressed = false;


    private void FixedUpdate()
    {
        //ũ�ν� �÷��� : �÷��� ���� �ڵ� ������

        
        //PC�� ������ �󿡼� �����ϴ� ��쿡 ���� ����
        #if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE
        HandleTouchInput(Input.mousePosition);
        #else
             HandleTouchInput(); 
        #endif
    }

    //�޼ҵ� �����ε�(Method Overloading)
    //�޼ҵ��� �Ű����� ����Ʈ(�ñ״�ó)�� �ٸ� ���, �̸��� ���Ƶ� �ٸ� �Լ��� ����մϴ�.
    private void HandleTouchInput(Vector3 mousePosition)
    {
        if (_buttonPressed)
        {
            //�Է¹��� ��ǥ ���
            Vector3 diff = mousePosition - _startPos;

            //Vector3.Distance(a,b) : �� �� ���� �Ÿ� ��� (������ �� ���) : ���� ���� © ��
            //��(dx�� + dy�� + dz��)

            //sqrMagnitude : �Ÿ��� ������ �� (�ܼ� ���) : Vector ���� �Ÿ� �� : ���� üũ
            //dx�� + dy�� + dz��
            if (diff.sqrMagnitude > _dragRadius * _dragRadius)
            {
                diff.Normalize(); //���� ���� �Ÿ� 1�� ����

                //���� ��Ʈ�ѷ� �����̱�
                _touchPad.position = _startPos + diff * _dragRadius;
            }
            else
            {
                //�Է� ������ ���� ��ǥ�� �ִ�ġ���� ũ���ʴٸ� ���� �Է� ��ǥ�� ����Ű �̵�
                _touchPad.position = mousePosition;
            }

            //����Ű�� �������� ���̸� ���
            Vector3 distance = _touchPad.position - _startPos;
            //���⸸ ���� ���
            Vector2 normalDiff = new Vector3(distance.x / _dragRadius, distance.y / _dragRadius, distance.z / _dragRadius);

            if (_player != null)
            {
                _player.OnStickChanged(normalDiff);
            }
        }

    }

    private void HandleTouchInput()
    {
        Debug.Log("����� ����");
    }
}
