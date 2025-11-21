using System.Runtime.CompilerServices;
using UnityEngine;

public class CheckGround : MonoBehaviour
{
    private bool onGround = false;
    [SerializeField] Animator animator;

    //외부 참조용 프로퍼티
    public bool OnGround
    {
        get
        {
            return onGround;
        }
    }

    //외부에서 애니메이션을 설정 가능하도록 작성하였으나, 현재는 SerializeField에 직접 넣어서 사용중
    public void SetAnimaotr(Animator animator)
    {
        this.animator = animator;
    }

    //땅에 닿을 경우 애니메이션 실행
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            onGround = true;
            animator.SetBool("IsGround", true);
        }
    }

    //땅에서 떨어질 경우 애니메이션 실행
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("IsGround", false);
            onGround = false;
            animator.SetTrigger("IsJump");
        }
    }

}
