using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Boss
{
    [SerializeField] float startScale;
    [SerializeField] float maxScale;

    int patternNum;
    private void Start()
    {
        patternNum = 0;
    }
    public override void Skill()
    {
        if (skillCoolTimer <= 0)
        {
            //첫번째 경고
            if (patternNum == 0 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.transform.localScale = Vector3.one * maxScale;
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f) * Time.deltaTime;
            }
            else if (patternNum == 0 && skillArea.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                skillArea.transform.localRotation = Quaternion.Euler(0, 0, -45);
                skillArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                patternNum = 1;
            }
            //두번째 경고
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f) * Time.deltaTime;
            }
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                skillArea.transform.localRotation = Quaternion.Euler(0, 0, 0);
                skillArea.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                patternNum = 2;

                StartCoroutine(SkillAct());
            }
            //첫번째 공격
            else if (patternNum == 2 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.7f) * Time.deltaTime;
            }
            else if (patternNum == 2 && skillArea.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                skillArea.transform.localRotation = Quaternion.Euler(0, 0, -45);
                skillArea.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                patternNum = 3;
                StartCoroutine(SkillAct());
            }
            //두번째 공격
            else if (patternNum == 3 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.7f) * Time.deltaTime;
            }
            else if (patternNum == 3 && skillArea.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                skillArea.transform.localRotation = Quaternion.Euler(0, 0, 0);
                skillArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                skillArea.transform.localScale = Vector3.one * startScale;
                patternNum = 0;
                skillCoolTimer = skillCool;

            }

        }

    }
}
