using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;              //�÷��̾�(����)
    PlayerHealth playerHealth;      //�÷��̾� ü��(������ ����)
    EnemyHealth enemyHealth;        //������ ü��(������ ����)
    bool playerInRange;             //�÷��̾���� �Ÿ�
    float timer;                    //�ð� üũ 


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Update()
    {
        //�ð� üũ
        timer += Time.deltaTime;

        //üũ�� �ð��� ���ð� �̻��̰� �÷��̾ ���� ���� �ְ� �� ü���� �����ִٸ�
        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack(); //����!!!
        }
    }
    private void Attack()
    {
        timer = 0; //Ÿ�� ����(�ٽ� ������ �� �ֵ���)

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

}
