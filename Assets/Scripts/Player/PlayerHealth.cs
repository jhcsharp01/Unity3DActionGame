using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startHealth = 100;       //�÷��̾��� ���� ü��
    public int currentHealth = 100;     //�÷��̾��� ���� ü��
    public Slider healthSlider;         //ü�� UI�� ����
    public Image damageImage;           //������ ���� ��쿡 ���� �̹���
    public AudioClip deathClip;         //�÷��̾� ������ ���� �� �� �����

    Animator anim;                      //�ִϸ�����
    AudioSource playerAudio;            //����� �ҽ�
    PlayerMovement playerMovement;      //�÷��̾� ������
    bool isDead;                        //���� Ȯ�ο� ����

    private void Awake()
    {
        //������Ʈ ����
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        //ü�� ����
        currentHealth = startHealth;
    }

    //�÷��̾ �������� �޾��� �� ȣ���� �Լ�
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
        else
        {
            anim.SetTrigger("Damage");
        }
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");
        playerMovement.enabled = false; //PlayerMovement�� ���� ��Ȱ��ȭ
    }
}
