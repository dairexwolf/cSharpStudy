// Для работы с каталогами в пространстве имен System.IO предназначены сразу два класса: Directory и DirectoryInfo.
//								Класс Directory
/*
Класс Directory предоставляет ряд статических методов для управления каталогами. Некоторые из этих методов:

- CreateDirectory(path): создает каталог по указанному пути path

- Delete(path): удаляет каталог по указанному пути path

- Exists(path): определяет, существует ли каталог по указанному пути path. Если существует, возвращается true, если не существует, то false

- GetDirectories(path): получает список каталогов в каталоге path

- GetFiles(path): получает список файлов в каталоге path

- Move(sourceDirName, destDirName): перемещает каталог

- GetParent(path): получение родительского каталога

*/

//									Класс DirectoryInfo
/*
Данный класс предоставляет функциональность для создания, удаления, перемещения и других операций с каталогами. Во многом он похож на Directory. Некоторые из его свойств и методов:

- Create(): создает каталог

- CreateSubdirectory(path): создает подкаталог по указанному пути path

- Delete(): удаляет каталог

- Свойство Exists: определяет, существует ли каталог

- GetDirectories(): получает список каталогов

- GetFiles(): получает список файлов

- MoveTo(destDirName): перемещает каталог

- Свойство Parent: получение родительского каталога

- Свойство Root: получение корневого каталога
*/

// Посмотрим на примерах применение этих классов:

// 1. Получение списка файлов и подкаталогов

string dirName = "C:\\";
if (Directory.Exists(dirName))
{
    Console.WriteLine("Подкаталоги:");
	
	// Получаем массив с названиями подкаталогов (папок в папке)
    string[] dirs = Directory.GetDirectories(dirName);
	// Перебираем массив
    foreach (string s in dirs)
        Console.WriteLine(s);
    Console.WriteLine();
    Console.WriteLine("Файлы");
	// Получаем массив названий файлов
    string[] files = Directory.GetFiles(dirName);
    foreach (string s in files)
        Console.WriteLine(s);
}

//!!! Обратите внимание на использование слешей в именах файлов. Либо мы используем двойной слеш: "C:\\", либо одинарный, но тогда перед всем путем ставим знак @: @"C:\Program Files"

// 2. Создание каталога

string path = @"C:\SomeDir";
string subpath = @"program\avalon";
DirectoryInfo dirInfo = new DirectoryInfo(path);
// Вначале проверяем, а нету ли такой директории, так как если она существует, то ее создать будет нельзя, и приложение выбросит ошибку.
if (!dirInfo.Exists)
	dirInfo.Create();
dirInfo.CreateSubdirectory(subpath);

//  В итоге у нас получится следующий путь: "C:\SomeDir\program\avalon"

// 3. Получение информации о каталоге

string dirName = "C:\\ProgramFiles";
DirectoryInfo dirInfo = new DirectoryInfo(dirName);
Console.WriteLine($"Название каталога: {dirinfo.Name}");
Console.WriteLine($"Полное название каталога: {dirinfo.FullName}");
Console.WriteLine($"Время создания каталога: {dirinfo.CreationTime}");
Console.WriteLine($"Корневой каталог: {dirinfo.Root}");

// 4. Удаление каталога
// Если мы просто применим метод Delete к непустой папке, в которой есть какие-нибудь файлы или подкаталоги, то приложение нам выбросит ошибку. Поэтому нам надо передать в метод Delete дополнительный параметр булевого типа, который укажет, что папку надо удалять со всем содержимым:

string dirName = @"C:\SomeFolder";

try
{
	DirectoryInfo dirInfo = new DirectoryInfo(dirName);
	dirInfo.Delete();
	Console.WriteLine("Каталог удален");
}
catch (Exception ex)
{
	Console.WriteLine(ex.Message);
}

// или так

string dir = "C:\\SomeFloder";

Directory.Delete(dir, true);

// 5. Перемещение каталога

string oldPath = @"C:\SomeFloder";
string newPath = "C:\\SomeDir";

DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
// проверяет сущетсвует ли "dirinfo" (существует), потом проверяет есть ли уже папка "C:\SomeDir", если эти два условия выполняются (существует объект (папка) и не существует вторая папа) => перемещаем первую папку во вторую. Перемещение значит crtrl + x, он вырезается (копируется и старый удаляется)
if (dirInfo.Exists && Directory.Exists(newPath) == false)
	dirInfo.MoveTo(newPath);
//	При перемещении надо учитывать, что новый каталог, в который мы хотим перемесить все содержимое старого каталога, не должен существовать.