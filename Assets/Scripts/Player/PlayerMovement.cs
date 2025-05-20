using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    PlayerAttack playerAttack;

    float lastAttackTime, lastSkillTime, lastDashTime;

    public bool attacking = false;
    public bool dashing = false;

    float h, v;

    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }

    private IEnumerator Attack()
    {
        if (Time.time - lastAttackTime > 1f)
        {
            lastAttackTime = Time.time;
            while (attacking)
            {
                animator.SetTrigger("Attack");
                playerAttack.NormalAttack();
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    public void OnAttackDown()
    {
        attacking = true;
        animator.SetBool("Combo", true);
        StartCoroutine(Attack());
    }
    public void OnAttackUp()
    {
        attacking = false;
    }
    public void OnSkillDown()
    {
        if (Time.time - lastSkillTime > 1.0f)
        {
            animator.SetBool("Skill", true);
            lastSkillTime = Time.time;
            playerAttack.SkillAttack();
        }
    }
    public void OnSkillUp()
    {
        animator.SetBool("Skill", false);
    }
    public void OnDashDown()
    {
        if (Time.time - lastDashTime > 1.0f)
        {
            //dashing = true;
            lastDashTime = Time.time;
            animator.SetTrigger("Dash");
            playerAttack.DashAttack();
        }
            
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttack>();    
    }

    // Update is called once per frame
    void Update()
    {
     
        if (animator)
        {
            //상대적인 방향 계싼, 캐릭터, 회전 /방향에 대한 처리
      //-------------------------------------------------------------------------------
            float back = 1f;

            if (v < 0f) back = -1f;
            animator.SetFloat("Speed", new Vector2(h, v).magnitude);
            animator.SetFloat("Direction", back * (Mathf.Atan2(h, v) * Mathf.Rad2Deg));
            // h , v 기준    back     position        
            // 0, 1           1          front           
            // 1 , 0          1          right             
            // 0, -1          -1          back
            // -1, -1         -1         left
      //-------------------------------------------------------------------------------
            Rigidbody rigidbody = GetComponent<Rigidbody>();

            if (rigidbody)
            {
                Vector3 speed = rigidbody.linearVelocity;
                speed.x = 4 * h;
                speed.z = 4 * v;
             
                if (h != 0f && v != 0f)
                {
                    transform.rotation = Quaternion.LookRotation(new Vector3(h, 0f, v));
                }
            }            
        }
    }
}
