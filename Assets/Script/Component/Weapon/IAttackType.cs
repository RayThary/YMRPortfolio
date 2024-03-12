using System.Collections.Generic;

public interface IAttackType
{
    void LeftDown();
    abstract void LeftUp();
    abstract bool Fire();
    float Rate { get; set; }
    float Timer { get; set; }   
}
