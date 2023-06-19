using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanEnermy : MonoBehaviour
{
    private Animator animator; // Animator ������Ʈ ����

    private void Start()
    {
        // Animator ������Ʈ�� ã�Ƽ� �Ҵ�
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        animator.SetBool("Hit", false);
    }
    public void PlayHitAnimation()
    {
        // �������� �¾��� �� ȣ��Ǵ� �Լ�

        // �ִϸ��̼� ����� ���� "Hit" Ʈ���Ÿ� ����
        animator.SetBool("Hit", true);
    }
}
