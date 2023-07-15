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
        //ĳ���� ������ġ, ���� ������, ĳ���ù���, ĳ���ñ���,����̾�
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        LimitAngleRange(angleRange);

        for (int i = 0; i < nearestTargets.Length; i++)
        {
            nearestTargets[i] = GetNearest();
        }
    }



    void LimitAngleRange(float angleRange)
    {
        //i�� 0���ͽ����ϸ� �迭���� �ε����� ���°������� ��ŵ�Ǵºκ��� �����!
        for (int i = targets.Length-1; i >= 0; i--)
        {
            // ���� player���ǰ������. 2d�ϱ�! player z���������� �������ؾ��ؼ� z�� �÷��̾�z��������.
            Vector3 interV = new Vector3(targets[i].transform.position.x - transform.position.x, targets[i].transform.position.y - transform.position.y, transform.position.z);

            float dot = Vector3.Dot(interV.normalized, transform.up); //transform.up���� 2d�ϱ�! �����ϼ�
            float theta = Mathf.Acos(dot);
            float degree = Mathf.Rad2Deg * theta;

            // �þ߰� �Ǻ�
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
        //�������Ÿ� targets�迭���� ���ش�. ���� �ι�° ����°.. ��������� ã�����ؼ� , �Ѹ����ۿ����������� �������� �� ä��!
        if (targets.Length > 1)
            targets = targets.Where((source, num) => num != resultNum).ToArray();

        return result;
    }

}
