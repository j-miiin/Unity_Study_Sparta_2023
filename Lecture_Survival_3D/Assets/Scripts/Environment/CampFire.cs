using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    public int damage;
    public float damageRate;

    // ������ �߿����� �ʰ�, ����/������ ���� �Ͼ �� HashSet�� ���� �ӵ��� �� ����
    private List<IDamagable> thingsToDamage = new List<IDamagable>();

    private void Start()
    {
        // �ٷ�(0) �����ϴµ�, damageRate �ֱ�� �����ϰڴ�
        InvokeRepeating("DealDamage", 0, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < thingsToDamage.Count; i++)
        {
            thingsToDamage[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable)) {
            thingsToDamage.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            thingsToDamage.Remove(damagable);
        }
    }
}
