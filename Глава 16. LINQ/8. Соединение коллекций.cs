// Соединение в LINQ используется для объединения двух разнотипных наборов в один. Для соединения используется оператор join или метод Join(). Как правило, данная операция применяется к двум наборам, которые имеют один общий критерий. Например, у нас есть два класса:

class Player
{
	public string Name { get; set; }
	public string Team { get; set; }
}

class Team
{
	public string Name { get; set; }
	public string Country { get; set; }
}

// Объекты обоих классов будет иметь один общий критерий - название команды. Соединим по этому критерию два набора этих классов:

List<Team> teams = new List<Team>()
{
	new Team { Name = "Na'Vi", Country = "Украина"},
	new Team { Name = "Virtus.Pro", Country = "Россия"}
};
List<Player> players = new List<Player>()
{
	new Player { Name = "Никита", Team = "Na'Vi" },
	new Player { Name = "Даниил", Team = "Na'Vi"},
	new Player { Name = "Максим", Team = "Virtus.Pro"}
};

var res = from pl in players join t in teams on pl.Team equals t.Name select new 
{ 
	Name = pl.Name, 
	Team = pl.Team, 
	Country = t.Country 
};
 // С помощью выражения join t in teams on pl.Team equals t.Name объект pl из списка players соединяется с объектом t из списка teams, если значение свойства pl.Team совпадает со значением свойства t.Name. Результатом соединения будет объект анонимного типа, который будет содержать три свойства.
 
foreach (var item in res)
	Console.WriteLine($"{item.Name} - {item.Team} ({item.Country})");

// То же самое действие можно было бы выполнить с помощью метода Join():

var result = /* первый набор */ players.Join(	teams,			// второй набор
							p => p.Team,	// свойство-селектор объекта из первого набора
							t => t.Name,	// свойство-селектор объекта из второго набора
							(p, t) => new	
							{
								Name = p.Name, 
								Team = p.Team, 
								Country = t.Country 
							}				// результат
							);

/* Метод Join() принимает четыре параметра:

второй список, который соединяем с текущим

свойство объекта из текущего списка, по которому идет соединение

свойство объекта из второго списка, по которому идет соединение

новый объект, который получается в результате соединения

*/
// 														GroupJoin
// Метод GroupJoin кроме соединения последовательностей также выполняет и группировку. Например, возьмем вышеопределенные списки teams и players и сгуппируем всех игроков по командам:

var result2 = teams.GroupJoin(
                                players,        // второй набор
                                t => t.Name,    // свойство-селектор объекта из первого набора
                                pl => pl.Team,  // свойство-селектор объекта из второго набора
                                (team, pls) => new  // результат - анонимный объект
                                {
                                    Name = team.Name,
                                    Country = team.Country,
                                    Players = pls.Select(p => p.Name)
                                });

        foreach (var team in result2)
        {
            Console.WriteLine($"{team.Name} : {team.Country}");
            foreach (string player in team.Players)
            {
                Console.WriteLine(player);
            }
            Console.WriteLine();
        }
// Метод GroupJoin, также как и метод Join, принимает все те же параметры. Только теперь во последний параметр - делегат передаются объект команды и набор игроков этой команды.

// 														Метод Zip

// Метод Zip позволяет объединять две последовательности таким образом, что первый элемент из первой последовательности объединяется с первым элементом из второй последовательности, второй элемент из первой последовательности соединяется со вторым элементом из второй последовательности и так далее:

List<Team> teams = new List<Team>()
{
	new Team { Name = "Na'Vi", Country = "Украина"},
	new Team { Name = "Virtus.Pro", Country = "Россия"},
	new Team { Name = "Liqud", Country = "США"}
};
List<Player> players = new List<Player>()
{
	new Player { Name = "Никита", Team = "Na'Vi" },
	new Player { Name = "Максим", Team = "Virtus.Pro"},
	new Player { Name = "SirGay", Team = "Liqud"}
};

var result = players.Zip(teams,
						(player, team) => new
						{
							Name = player.Name,
							Team = team.Name, Country = team.Country
						});
foreach (var player in result)
{
	Console.WriteLine($"{player.Name} : {player.Team} : ({player.Country})");
	Console.WriteLine();
}

// Метод Zip в качестве первого параметра принимает вторую последовательность, с которой надо соединяться, а в качестве второго параметра - делегат для создания нового объекта.