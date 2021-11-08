// При запуске приложения операционная система создает для него отдельный процесс, которому выделяется определённое адресное пространство в памяти и который изолирован от других процессов. Процесс может иметь несколько потоков. Как минимум, процесс содержит один - главный поток. В приложении на C# точкой входа в программу является метод Main. Вызов этого метода автоматически создает главный поток. А из главного потока могут запускаться вторичные потоки.
// В .NET процесс представлен классом Process из пространства имен System.Diagnostics. Этот класс позволяет управлять уже запущенными процессами, а также запускать новые. В данном классе определено ряд свойств и методов, позволяющих получать информацию о процессах и управлять ими:

/*
Свойство Handle: возвращает дескриптор процесса

Свойство Id: получает уникальный идентификатор процесса в рамках текущего сеанса ОС

Свойство MachineName: возвращает имя компьютера, на котором запущен процесс

Свойство Modules: получает доступ к коллекции ProcessModuleCollection, которая хранит набор модулей (файлов dll и exe), загруженных в рамках данного процесса

Свойство ProcessName: возвращает имя процесса, которое нередко совпадает с именем приложения

Свойство StartTime: возвращает время, когда процесс был запущен

Свойство VirtualMemorySize64: возвращает объем памяти, который выделен для данного процесса

Метод CloseMainWindow(): закрывает окно процесса, который имеет графический интерфейс

Метод GetProcesses(): возвращающий массив всех запущенных процессов

Метод GetProcessesByName(): возвращает процессы по его имени. Так как можно запустить несколько копий одного приложения, то возвращает массив

Метод Kill(): останавливает процесс

Метод Start(): запускает новый процесс

*/

// Получим все запущенные процессы:

using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        foreach (Process process in Process.GetProcesses())
            // выводим id и имя процесса
            Console.WriteLine($"ID: {process.Id}   Name: {process.ProcessName}");

        // Получим id процесса, представляющего Visual Studio:
        Process proc = Process.GetProcessesByName("devenv")[0];
        Console.WriteLine($"ID {proc.Id}");

    }
}

//														Потоки процесса
// Свойство Threads представляет коллекцию потоков процесса - объект ProcessThreadCollection, каждый поток в которой является объектом ProcessThread. В данном классе можно выделить следующие свойства:
/*

CurrentPriority: возвращает текущий приоритет потока

Id: идентификатор потока

IdealProcessor: позволяет установить процессор для обработки потока

PriorityLevel: уровень приоритета потока

StartAddress: адрес в памяти функции, запустившей поток

StartTime: время запуска потока

*/

 // Например, получим все потоки процесса Visual Studio:
 
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Process proc = Process.GetProcessesByName("devenv")[0];
		ProcessThreadCollection processThreads = proc.Threads;
		
		foreach (ProcessThread thread in processThreads)
			Console.WriteLine($"ThreadID: {thread.Id}   StartTime: {thread.StartTime}");

    }
}

//													Модули процесса
// Одно приложение может использовать набор различных сторонних библиотек и модулей. Для их получения класс Prosess имеет свойство Modules, которое представляет объект ProcessModuleCollection. Каждый отдельный модуль представлен классом ProcessModule, у которого можно выделить следующие свойства:

/*
BaseAddress: адрес модуля в памяти

FileName: полный путь к файлу модуля

EntryPointAddress: адрес функции в памяти, которая запустила модуль

ModuleName: название модуля (краткое имя файла)

ModuleMemorySize: возвращает объем памяти, необходимый для загрузки модуля
*/

// Получим все модули, используемые Visual Studio:

using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Process proc = Process.GetProcessesByName("devenv")[0];
		ProcessModuleCollection modules = proc.Modules;
		
		foreach(ProcessModule module in modules)
			Console.WriteLine($"Name: {module.ModuleName}   MemorySize: {module.ModuleMemorySize}");

    }
}

// 														Запуск нового процесса
// С помощью статического метода Process.Start() можно запустить новый процесс. Например:

using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        Process.Start("C://Program Files (x86)//Notepad++//notepad++.exe");
		
		
		
		
    }
}

// При обращении к исполняемому файлу exe запускает приложение. 

// Однако при запуске некоторых программ может потребоваться передать им различные параметры. В этом случае можно использовать перегруженную версию метода, передавая в качестве второго параметра параметры:

Process.Start("C://Program Files (x86)//Notepad++//notepad++.exe", @"C:\SomeFolder\htc.txt");

// Чтобы отделить настройку параметров запуска от самого запуска можно использовать класс ProcessStartInfo:

ProcessStartInfo procInfo = new ProcessStartInfo();
// исполняемый файл программы - нотепад++
procInfo.FileName = "C://Program Files (x86)//Notepad++//notepad++.exe";
// аргумент запуска - адрес до этого файла
// procInfo.Arguments = @"C:\Users\vyatkin\Documents\Visual Studio 2015\C# мое\Глава 24. Процессы и домены приложения\1. Процесс.cs";
// русский не поддерживается =(
procInfo.Arguments = @"C:\SomeFolder\users.xml";
Process.Start(procInfo);




