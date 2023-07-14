using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Boss
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
        if (skillCoolTimer > 0)
        {
            LookPlayer();
        }
        if (skillCoolTimer <= 0)
        {
            if (skillArea.transform.localScale.y < 20 && patternNum == 0)
            {
                //스케일이 늘어나는 속도에따라서 위쪽방향으로 움직이게// 스케일이 2배빨리늘어나야속도가맞음.
                skillArea.transform.localScale += 2 * Vector3.up * Time.deltaTime * warningSpeed;
                skillArea.transform.Translate(Vector3.up * Time.deltaTime * warningSpeed);
            }
            else if (skillArea.transform.localScale.y >= 20 && patternNum == 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color = Color.white;
                StartCoroutine(SkillAct());
                patternNum = 1;
            }
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a > 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.5f) * Time.deltaTime;
            }
            //초기화
            else if (patternNum == 1 && skillArea.GetComponent<SpriteRenderer>().color.a <= 0)
            {
                skillArea.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
                skillArea.transform.localScale = new Vector3(skillArea.transform.localScale.x, startScale, skillArea.transform.localScale.z);
                skillArea.transform.localPosition = Vector3.zero;
                patternNum = 0;

                skillCoolTimer = skillCool;
            }
        }

    }

    public void LookPlayer()
    {
        Vector2 direction = (GameManager.instance.player.gameObject.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 270, Vector3.forward);

        skillArea.transform.rotation = q;
    }
}
