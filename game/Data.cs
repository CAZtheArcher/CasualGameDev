using Godot;


[GlobalClass]
public partial class Data : Resource
{

    [Export]
    public int level;

    public int ChangeLevel() { level++; return level; }
}
