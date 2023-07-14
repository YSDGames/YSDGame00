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
        //����˸� ���� ����.
        if (skillCoolTimer <= 0)
        {
            if (skillArea.transform.localScale.x < maxScale && patternNum == 0)
            {
                skillArea.GetComponent<Collider2D>().enabled = false;
                skillArea.transform.localScale += Vector3.one * Time.deltaTime* warningSpeed;
            }
            // ��ų �ߵ��� ��Ÿ�� �ʱ�ȭ
            else if (skillArea.transform.localScale.x >= maxScale && patternNum == 0)
            {
                StartCoroutine(SkillAct());
                patternNum = 1;
            }
            // �ߵ��� ��ų �̹��� ���İ� ����.
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.7f) * Time.deltaTime;
            }
            // ���İ� ��� ������ �ʱⰪ���� ���ư�.
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
