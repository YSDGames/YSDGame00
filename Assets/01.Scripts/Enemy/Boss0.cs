using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss0 : Boss
{
    [SerializeField] float startScale;
    [SerializeField] float maxScale;
    [SerializeField] float warningSpeed;
    int patternNum;

    private void Start()
    {
        patternNum = 0;
    }
    public override void Skill()
    {
        //위험알림 영역 생성.
        if (skillCoolTimer <= 0)
        {
            if (skillArea.transform.localScale.x < maxScale && patternNum == 0)
            {
                skillArea.GetComponent<Collider2D>().enabled = false;
                skillArea.transform.localScale += Vector3.one * Time.deltaTime* warningSpeed;
            }
            // 스킬 발동및 쿨타임 초기화
            else if (skillArea.transform.localScale.x >= maxScale && patternNum == 0)
            {
                StartCoroutine(SkillAct());
                patternNum = 1;
            }
            // 발동후 스킬 이미지 알파값 감소.
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.7f) * Time.deltaTime;
            }
            // 알파값 모두 감소후 초기값으로 돌아감.
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                skillArea.transform.localScale = Vector3.one * startScale;
                skillArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                skillCoolTimer = skillCool;
                patternNum = 0;
            }
        }
        
    }

}
