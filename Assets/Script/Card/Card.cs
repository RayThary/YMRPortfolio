public abstract class Card
{
    //�� ī�带 ������ ���� (�߻�� ȿ���� �ο��ϱ� ����)
    protected Weapon launcher;
    public Player user;
    //�׳� ����� ����� ���� ����
    protected float figure;

    protected string exp = "";
    public string Explanation { get { return exp; } }

    //ī���� ȿ��
    public abstract void Impact();

    public virtual void Activation(Weapon launcher, Player player)
    {
        this.launcher = launcher;
        user = player;
    }
    public abstract void Deactivation();
}