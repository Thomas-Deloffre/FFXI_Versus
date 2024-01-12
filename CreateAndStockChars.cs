Tarutaru Tarutaru = new Tarutaru();
BlackMage BlackMage = new BlackMage();

Fighter Iroha = new Fighter(
1,
"Iroha",
new Hume(),
new Ninja(),
"Your beloved student from the future who comes back to save us all.",
"Master, together we will purge Evil from this world !",
27,
new string[] { "Lion, Arciela" },
2700,
1850,
150,
140,
160,
170,
150,
120,
110
);

Fighter Shantotto = new Fighter(
2,
"Shantotto",
new Tarutaru(),
new BlackMage(),
"A black mage from the Federation of Windurst and a hero of the Crystal War.",
"O-hohohohoho ! Let us see what you're good at !",
50,
new string[] { "Ajido-Marujido", "Koru-Moru", "Yoran-Oran" },
2700,
1780,
150,
140,
160,
170,
150,
120,
120
);

//Add character to the Char datasheet
Console.WriteLine("Adding character to the datasheet..");
characters.Add(Iroha);
characters.Add(Shantotto);

//Save updates of the datasheet
Console.WriteLine("Saving changes in the datasheet..");
CharacterManager.SaveCharacters(characters);
