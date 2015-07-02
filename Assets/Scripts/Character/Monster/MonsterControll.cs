using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    private Animator animator;
	private CharacterInformation characterInfor;
	private PlayerControl player;
	private CharacterCommon characterCommon;

    public void Attack(bool flag)
    {
		characterCommon.Attack(flag);
    }

    public void Hurt()
    {
		characterCommon.Hurt();
    }

    public void LookAround(bool flag)
    {
        
    }
	
    void Awake()
    {
        animator = GetComponent<Animator>();
		characterInfor = GetComponent<CharacterInformation>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		characterCommon = GetComponent<CharacterCommon>();
    }
}
