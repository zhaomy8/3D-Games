using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float explosionRadius = 3.0f;
    private TankType tankType;

    //设置发射子弹的坦克类型
    public void setTankType(TankType type)
    {
        tankType = type;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CollisionEnder");
        if (collision.transform.gameObject.tag == "tankEnemy" && this.tankType == TankType.ENEMY ||
            collision.transform.gameObject.tag == "tankPlayer" && this.tankType == TankType.PLAYER)
        {
            return;
        }
        MyFactory factory = Singleton<MyFactory>.Instance;
        ParticleSystem explosion = factory.getParticleSystem();
        explosion.transform.position = gameObject.transform.position;
        //获取爆炸范围内的所有碰撞体
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, explosionRadius);

        foreach (var collider in colliders)
        {
            float hurt;
            // 如果是玩家发出的子弹伤害高一点
            if (collider.tag == "tankEnemy" && this.tankType == TankType.PLAYER)
            {
                hurt = 300.0F;
                collider.GetComponent<Tank>().setHP(collider.GetComponent<Tank>().getHP() - hurt);
                Debug.Log("Enemy Hp: ");
            }
            else if (collider.tag == "tankPlayer" && this.tankType == TankType.ENEMY)
            {
                hurt = 100.0F;
                collider.GetComponent<Tank>().setHP(collider.GetComponent<Tank>().getHP() - hurt);
                Debug.Log("Player Hp: ");
            }
            explosion.Play();
        }

        if (gameObject.activeSelf)
        {
            factory.recycleBullet(gameObject);
        }
    }

}