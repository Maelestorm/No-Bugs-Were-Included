using UnityEditor;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private CircleCollider2D tongueTipAttackCollider;
    [SerializeField] private CircleCollider2D bossMeleeAttackCollider;

    public static float bossTongueColliderDamage = 10;
    public static float bossMeleeColliderDamage = 20;

}
