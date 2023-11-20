public abstract class Card
{
    //�� ī�带 ������ ���� (�߻�� ȿ���� �ο��ϱ� ����)
    protected Launcher launcher;
    //�׳� ����� ����� ���� ����
    protected float figure;

    //ī���� ȿ��
    public abstract void Impact();
    
    public virtual void Activation(Launcher launcher)
    {
        this.launcher = launcher;
    }
    public abstract void Deactivation();
}

// �߻��Ҷ����� ȣ���������� �Ѵ�
// Impact�� ���ϴ� ���� �ۼ��� Activation�� 
// launcher.FireCallbackAdd(Impact); �ۼ�
// Deactivation�� 
// launcher.FireCallbackRemove(Impact); �ۼ�

// �Ѿ��� ��뿡�� ���������� ȣ�� ������ �Ѵ�
// �Ű������� Unit unit, float figure�� �Լ��� �ϳ� �����
// �̶� unit�� ���� ���� �����ߴ��� figure�� �� ��ġ�� �������� ���� ����
// ���ϴ� ���� �ۼ��� Activation��
// player.stat.AddAttack(�����Լ��̸�); �ۼ�
// Deactivation��
// player.stat.RemoveAttack(�����Լ��̸�); �ۼ�

// ���� ���������� ������� �Ծ����� ȣ�� ������ �Ѵ�
// �Ű������� Unit unit, float figure�� �Լ��� �ϳ� �����
// �̶� unit�� ���� �������� ���ݹ޾Ҵ��� figure�� �� ��ġ�� �������� ���� ����
// ���ϴ� ���� �ۼ��� Activation��
// player.stat.AddHit(�����Լ��̸�); �ۼ�
// Deactivation��
// player.stat.RemoveHit(�����Լ��̸�); �ۼ�

//����
//public class ThreeStrokeAttack : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 0;
//        launcher.FireCallbackAdd(Impact);
//    }

//    public override void Deactivation()
//    {
//        launcher.FireCallbackRemove(Impact);
//    }

//    public override void Impact()
//    {
//        figure++;
//        if (figure >= 3)
//        {
//            SpecialFire(30);
//            SpecialFire(-30);
//            figure = 0;
//        }
//    }

//    public void SpecialFire(float angle)
//    {
//        Bullet b = launcher.GetBullet();
//        b.gameObject.SetActive(true);
//        b.transform.position = launcher.muzzle.position;
//        b.transform.localEulerAngles = new Vector3(0, launcher.launcher.eulerAngles.y - 90 - 90 + angle, 0);
//        b.Straight();
//    }
//}

//public class PoisonBullet : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 3;
//        GameManager.instance.GetPlayer.stat.AddAttack(Poison);
//    }
//    public override void Deactivation()
//    {
//        GameManager.instance.GetPlayer.stat.RemoveAttack(Poison);
//    }

//    public override void Impact()
//    {

//    }

//    public void Poison(Unit unit, float figure)
//    {
//        unit.stat.Be_Attacked_Poison(5, this.figure, GameManager.instance.GetPlayer);
//    }
//}

//public class Bloodsucking : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 1;
//        GameManager.instance.GetPlayer.stat.AddAttack(Blood);
//    }
//    public override void Deactivation()
//    {
//        GameManager.instance.GetPlayer.stat.RemoveAttack(Blood);
//    }

//    public override void Impact()
//    {

//    }

//    public void Blood(Unit unit, float f)
//    {
//        GameManager.instance.GetPlayer.stat.RecoveryHP(figure, GameManager.instance.GetPlayer);
//    }
//}

//public class Armor_Of_Thorns : Card
//{
//    public override void Activation(Launcher launcher)
//    {
//        base.Activation(launcher);
//        figure = 1;
//        GameManager.instance.GetPlayer.stat.AddHit(Thorns);
//    }
//    public override void Deactivation()
//    {
//        GameManager.instance.GetPlayer.stat.RemoveHit(Thorns);
//    }

//    public override void Impact()
//    {

//    }

//    public void Thorns(Unit unit, float f)
//    {
//        unit.stat.Be_Attacked_TRUE(figure, GameManager.instance.GetPlayer);
//    }
//}


