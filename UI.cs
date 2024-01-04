using Godot;
using System;

public partial class UI : Node2D
{

	private TextEdit depositEdit;
    private TextEdit spinForEdit;
	private Button startButton;

    // Variables to store user input
    private float deposit = 0f;
    private float spinFor = 0f;
	
	//private LineEdit myTextEdit;

	private TextEdit debugEdit;

	private bool insatInput = false;

	//Opret tom sprite
	private Sprite2D sp1;
	private Sprite2D sp2;
	private Sprite2D sp3;

	//Henvisninger til alle billederne
	Texture2D tex1 =  ResourceLoader.Load("res://billeder/7.png") as Texture2D;
	Texture2D tex2 =  ResourceLoader.Load("res://billeder/banan.png") as Texture2D;
	Texture2D tex3 =  ResourceLoader.Load("res://billeder/dia.png") as Texture2D;
	Texture2D tex4 =  ResourceLoader.Load("res://billeder/hestesko.png") as Texture2D;
	Texture2D tex5 =  ResourceLoader.Load("res://billeder/jordbær.png") as Texture2D;
	Texture2D tex6 =  ResourceLoader.Load("res://billeder/klokke.png") as Texture2D;
	Texture2D tex7 =  ResourceLoader.Load("res://billeder/orange.png") as Texture2D;
	Texture2D tex8 =  ResourceLoader.Load("res://billeder/vandmelon.png") as Texture2D;
	Texture2D tex9 =  ResourceLoader.Load("res://billeder/vindruer.png") as Texture2D;

	//Opret liste
	private Texture2D[] billedeListe;

	//Opret point liste
	private int[] pointListe = {3, 2, 10, 7, 3, 4, 1, 2, 3};

	//Opret rekord value;

	float rekord;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		//Indsæt i listen "billedeListe"
		billedeListe = new Texture2D[] {tex1, tex2, tex3, tex4, tex5, tex6, tex7, tex8, tex9};

		// Assuming LineEdit nodes are direct children of the Control node
		//Deposit boks henvisning
		depositEdit = GetNode<TextEdit>("Control/Label-deposit/TextEdit-deposit");
    
		//Start knap henvisning
		startButton = GetNode<Button>("Control/Button");

		//Spin-for boks henvisning
		spinForEdit = GetNode<TextEdit>("Control/Label-spinfor/TextEdit2-spin-for");

		//Debug-boks henvisning
		debugEdit = GetNode<TextEdit>("Control/Maskine hjælp/TextEdit-deposit");

		//Henvis to sp'erne
		sp1 = GetNode<Sprite2D>("Sprite 1");
		sp2 = GetNode<Sprite2D>("Sprite 2");
		sp3 = GetNode<Sprite2D>("Sprite 3");
		
	}
	
	private void _on_button_pressed()
    {
		//Konverter string i "Deposit" boks til float og sæt "deposit" til den float
        float depositValue = float.TryParse(depositEdit.Text, out float Depositresult) ? Depositresult : 0.0f;
		deposit = depositValue;

		//Konverter string i "spinFor" boks til float og sæt "spinFor" til den float
		float spinValue = float.TryParse(spinForEdit.Text, out float Spinresult) ? Spinresult : 0.0f;
		spinFor = spinValue;

        GD.Print("Depositing: ", deposit);
		GD.Print("Spinning for: ", spinValue);

		//kald funktion som spinner maskinen
		insatInput = true;

    }

	private void _spin_that_shit()
	{
		if (insatInput)
		{
			//Lav 3 random værdier mellem 1 og 9
			int randomFloat1 = (int)GD.RandRange(1, 9);
			int randomFloat2 = (int)GD.RandRange(1, 9);
			int randomFloat3 = (int)GD.RandRange(1, 9);

			//Ændre de 3 tomme sprites
			sp1.Texture = billedeListe[randomFloat1];
			sp2.Texture = billedeListe[randomFloat2];
			sp3.Texture = billedeListe[randomFloat3];

			//Udreng point resultat og tilsæt det til deposit
			float pointsFået = (spinFor * 0.5f) * pointListe[randomFloat1] * pointListe[randomFloat2] * pointListe[randomFloat3];

			//Træk "spinfor" værdien fra deposit
			deposit -= spinFor;

			//Opdater deposit value i boksen og floaten
			deposit += pointsFået;
			depositEdit.Text = deposit.ToString();

		}
		else
		{
			debugEdit.Text = "Du skal trykke på start-knappen før du kan spinne maskinen :)";
		}
		
		

	}

	private void _restartGame()
	{
		//Restart the game
		deposit = 0;
		spinFor = 0;

		depositEdit.Text = "";
		spinForEdit.Text = "";

		insatInput = false;

		sp1.Texture = null;
		sp2.Texture = null;
		sp3.Texture = null;

		debugEdit.Text = "";

	}

	void rekordkontrol()
	{
		if (deposit > rekord)
		{
			rekord = deposit;
			debugEdit.Text = "Ny rekord!";
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (deposit <= 0 && insatInput)
		{
			rekordkontrol();
			debugEdit.Text = "BUZZ";
		}
		

	}
}
