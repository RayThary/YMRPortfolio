public abstract class Card
{
    //�� ī�带 ������ ���� (�߻�� ȿ���� �ο��ϱ� ����)
    protected Launcher launcher;
    public Unit user;
    //�׳� ����� ����� ���� ����
    protected float figure;

    protected string exp = "";
    public string Explanation { get { return exp; } }

    //ī���� ȿ��
    public abstract void Impact();
    
    public virtual void Activation(Launcher launcher, Unit unit)
    {
        this.launcher = launcher;
        user = unit;
    }
    public abstract void Deactivation();
}
