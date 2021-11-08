using System;
class Progarm
{
    static void Main()
    {
        int zarplata = 22000;
        int sr = 200;           // среднее за обед
        int mar = 40 * 2;       // маршрутка
        //int metro = 28;         // метро
        int water = 10;         // вода в качалку

        int week = (sr + mar) * 3 + mar * 3 + water * 3;
        Console.WriteLine("За неделю: " + week);
        double mounth = week * 5;
        Console.WriteLine("За месяц: " + mounth);
        int random = 2000;
        int all_plata_mounth = random + 3000 + (int)mounth;
        Console.WriteLine("За месяц, учитывая собственные случайные расходы: " + all_plata_mounth);
        int res = zarplata - all_plata_mounth;
        Console.WriteLine("Прирост деняг в месяц: " + res);
    }
}