public abstract class Card
{
    //이 카드를 소유한 무기 (발사시 효과를 부여하기 위해)
    protected Launcher launcher;
    public Unit user;
    //그냥 쓰라고 만들어 놓은 변수
    protected float figure;

    protected string exp = "";
    public string Explanation { get { return exp; } }

    //카드의 효과
    public abstract void Impact();
    
    public virtual void Activation(Launcher launcher, Unit unit)
    {
        this.launcher = launcher;
        user = unit;
    }
    public abstract void Deactivation();
}
