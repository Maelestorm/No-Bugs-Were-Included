using UnityEditor;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private CircleCollider2D tongueTipAttackCollider;
    [SerializeField] private CircleCollider2D bossMeleeAttackCollider;

    public static float bossTongueColliderDamage;
    public static float bossMeleeColliderDamage;

 
    //animation event calls for bossMeleeAttackCollider
    public void BossMeleeColliderActivate()
    {
        bossMeleeAttackCollider.gameObject.SetActive(true);
    }
    public void BossMeleeColliderDeActivate()
    {
        bossMeleeAttackCollider.gameObject.SetActive(false);
    }

    //animation event calls for tongueTipAttackCollider
    public void TongueTipAttackColliderActivate()
    {
        tongueTipAttackCollider.gameObject.SetActive(true);
    }
    public void TongueTipAttackColliderDeActivate()
    {
        tongueTipAttackCollider.gameObject.SetActive(false);
    }
}
