using Godot;


[GlobalClass]
public partial class Data : Resource
{

    [Export]
    int level;

    public void ChangeLevel() { level++; }
}
