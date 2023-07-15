using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public float angleRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] targets;

    public Transform[] nearestTargets;

    private void Awake()
    {
        nearestTargets = new Transform[6];
    }



    private void FixedUpdate()
    {
        //캐스팅 시작위치, 원의 반지름, 캐스팅방향, 캐스팅길이,대상레이어
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        LimitAngleRange(angleRange);

        for (int i = 0; i < nearestTargets.Length; i++)
        {
            nearestTargets[i] = GetNearest();
        }
    }



    void LimitAngleRange(float angleRange)
    {
        //i를 0부터시작하면 배열에서 인덱스를 뺴는과정에서 스킵되는부분이 생긴다!
        for (int i = targets.Length-1; i >= 0; i--)
        {
            // 적과 player와의각도계산. 2d니까! player z값기준으로 각도정해야해서 z를 플레이어z값으로함.
            Vector3 interV = new Vector3(targets[i].transform.position.x - transform.position.x, targets[i].transform.position.y - transform.position.y, transform.position.z);

            float dot = Vector3.Dot(interV.normalized, transform.up); //transform.up으로 2d니까! 조심하셈
            float theta = Mathf.Acos(dot);
            float degree = Mathf.Rad2Deg * theta;

            // 시야각 판별
            if (degree > angleRange / 2f)
                targets = targets.Where((source, num) => num != i).ToArray();
        }
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;
        int resultNum = 0;
        int i = 0;
        foreach (RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if (curDiff < diff)
            {
                resultNum = i;
                diff = curDiff;
                result = target.transform;
            }
            i++;
        }
        //젤가까운거를 targets배열에서 빼준다. 이후 두번째 세번째.. 가까운적을 찾기위해서 , 한마리밖에남지않으면 그적으로 다 채움!
        if (targets.Length > 1)
            targets = targets.Where((source, num) => num != resultNum).ToArray();

        return result;
    }

}
