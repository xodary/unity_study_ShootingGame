using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class humanEnermy : MonoBehaviour
{
    private Animator animator; // Animator 컴포넌트 참조

    private void Start()
    {
        // Animator 컴포넌트를 찾아서 할당
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        animator.SetBool("Hit", false);
    }
    public void PlayHitAnimation()
    {
        // 레이저에 맞았을 때 호출되는 함수

        // 애니메이션 재생을 위해 "Hit" 트리거를 설정
        animator.SetBool("Hit", true);
    }
}
