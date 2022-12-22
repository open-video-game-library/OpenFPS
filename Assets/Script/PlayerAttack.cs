using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject damage;

    [SerializeField] private GameObject parentObject;

    public GameObject thisParent;

    public GameObject prefab;
    Ray ray;
    RaycastHit hit;

    Transform WorldCanvas;

    public GameObject Hiteffect;

   //public bool enemybullet;

    // Start is called before the first frame update
    void Start()
    {
        WorldCanvas = GameObject.Find("WorldCanvas").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
            Vector3 hitPos;

            foreach (ContactPoint point in other.contacts)
            {
                hitPos = point.point;
                Quaternion rot = Quaternion.FromToRotation(-1 * prefab.transform.forward, point.normal);
                Vector3 pos = hitPos + (point.normal * 0.01f);
                GameObject dankon = Instantiate(prefab, pos, rot);
                Destroy(dankon);
                GameObject particle = Instantiate(Hiteffect, pos, rot);
                particle.GetComponent<ParticleSystem>().Play();
                Destroy(particle, 1f); // 1f後にパーティクルは消滅します

                if (other.gameObject.tag == "Enemy" && thisParent.transform.tag == "Player")
                {

                    Wave.hitmany++;
                    if (other.transform.root.GetComponent<EnemyAI>() != null)
                    {
                        GameObject a = Instantiate(damage, new Vector3(pos.x, pos.y + 1f, pos.z), Quaternion.identity, WorldCanvas);
                        a.GetComponent<Text>().text = " ";
                        if (other.transform.root.GetComponent<EnemyAI>().HP > 0)
                        {
                            
                            if (other.transform.name.Contains("Head"))
                            {
                                other.transform.root.GetComponent<EnemyAI>().GetDamage(5);
                                Wave.Score += 50;
                                a.GetComponent<Text>().text = "+50";
                                a.GetComponent<Text>().color = new Color(1f, 0f, 0f, 1f);
                            }
                            else
                            {
                                other.transform.root.GetComponent<EnemyAI>().GetDamage(1);
                                Wave.Score += 10;
                                a.GetComponent<Text>().text = "+10";
                            }
                        }
                        Wave.hitCount += 1;
                        float dist = Vector3.Distance(this.transform.position, thisParent.transform.position);
                        break;
                    }
                }
                else if(other.gameObject.tag == "Player" && thisParent.transform.tag == "Enemy")
                {
                    PlayerController.hp -= 1;
                    break;
                }
                
            }

        this.gameObject.SetActive(false);
    }

        void OnTriggerEnter(Collider other)
    {

    }
}
