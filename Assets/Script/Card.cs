public abstract class Card
{
    //이 카드를 소유한 무기 (발사시 효과를 부여하기 위해)
    protected Launcher launcher;
    //그냥 쓰라고 만들어 놓은 변수
    protected float figure;

    //카드의 효과
    public abstract void Impact();
    
    public virtual void Activation(Launcher launcher)
    {
        this.launcher = launcher;
    }
    public abstract void Deactivation();
}

// 발사할때마다 호출해줬으면 한다
// Impact에 원하는 내용 작성후 Activation에 
// launcher.FireCallbackAdd(Impact); 작성
// Deactivation에 
// launcher.FireCallbackRemove(Impact); 작성

// 총알이 상대에게 명중했을때 호출 했으면 한다
// 매개변수가 Unit unit, float figure인 함수를 하나 만들고
// 이때 unit은 내가 누굴 공격했는지 figure는 그 수치는 얼마인지에 대한 정보
// 원하는 내용 작성후 Activation에
// player.stat.AddAttack(만든함수이름); 작성
// Deactivation에
// player.stat.RemoveAttack(만든함수이름); 작성

// 내가 누군가에게 대미지를 입었을때 호출 했으면 한다
// 매개변수가 Unit unit, float figure인 함수를 하나 만들고
// 이때 unit은 내가 누구에게 공격받았는지 figure는 그 수치는 얼마인지에 대한 정보
// 원하는 내용 작성후 Activation에
// player.stat.AddHit(만든함수이름); 작성
// Deactivation에
// player.stat.RemoveHit(만든함수이름); 작성

//예시
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


