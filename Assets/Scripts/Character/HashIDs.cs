using UnityEngine;

public class HashIDs : MonoBehaviour
{
    #region State Hash

    public int IdleState;
    public int RunState;
    public int BornState;
    public int JumpState;
    public int Jump1State;
    public int AttackState;
    public int AttackStayState;
    public int Attack1State;
    public int Attack2State;
    public int Attack3State;
    public int Attack4State;
    public int SkillQState;
    public int SkillEState;
    public int HurtState;
    public int ShortState;
    public int DuangState;
    public int DuangeDieState;
    public int DieState;

    #endregion

    #region Parameters

    public int DoesJump;
    public int Speed;
    public int IsDie;
    public int DoesAttack;
    public int DoesHurt;
    public int CurrentStateTime;
    public int IsDuang;
    public int IsDuangeDie;
    public int IsSkillQ;
    public int IsSkillW;
    public int IsSkillE;

    #endregion

    void Awake()
    {
        IdleState = Animator.StringToHash("Base Layer.Idle");
        RunState = Animator.StringToHash("Base Layer.Run");
        BornState = Animator.StringToHash("Base Layer.Born");
        JumpState = Animator.StringToHash("Base Layer.Jump");
        Jump1State = Animator.StringToHash("Base Layer.Jump 1");
        AttackState = Animator.StringToHash("Base Layer.Attack");
        AttackStayState = Animator.StringToHash("Base Layer.AttackStay");
        Attack1State = Animator.StringToHash("Base Layer.Attack1");
        Attack2State = Animator.StringToHash("Base Layer.Attack2");
        Attack3State = Animator.StringToHash("Base Layer.Attack3");
        Attack4State = Animator.StringToHash("Base Layer.Attack4");
        SkillQState = Animator.StringToHash("Base Layer.SkillQ");
        SkillEState = Animator.StringToHash("Base Layer.SKillE");
        HurtState = Animator.StringToHash("Base Layer.Hurt");
        ShortState = Animator.StringToHash("Base Layer.Short");
        DuangState = Animator.StringToHash("Base Layer.Duang");
        DuangeDieState = Animator.StringToHash("Base Layer.DuangeDie");
        DieState = Animator.StringToHash("Base Layer.Die");

        DoesJump = Animator.StringToHash("Jump");
        Speed = Animator.StringToHash("Speed");
        IsDie = Animator.StringToHash("Die");
        DoesAttack = Animator.StringToHash("Attack");
        DoesHurt = Animator.StringToHash("Hurt");
        CurrentStateTime = Animator.StringToHash("currentStateTime");
        IsDuang = Animator.StringToHash("Duang");
        IsDuangeDie = Animator.StringToHash("DuangDie");
        IsSkillQ = Animator.StringToHash("SkillQ");
        IsSkillE = Animator.StringToHash("SkillE");
        IsSkillW = Animator.StringToHash("SkillW");
    }
}
