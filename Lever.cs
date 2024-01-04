using Godot;
using System;

public partial class Lever : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	// Called when input is detected (e.g., mouse/keyboard interaction)
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.Pressed && eventMouseButton.ButtonIndex == MouseButton.Left)
        {
            if (GetRect().HasPoint(ToLocal(eventMouseButton.Position)))
            {
                GD.Print("You clicked on Sprite!");
            }
        }
    }
 

}
