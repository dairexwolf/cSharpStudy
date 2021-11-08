// Подобно паре Directory/DirectoryInfo для работы с файлами предназначена пара классов File и FileInfo. С их помощью мы можем создавать, удалять, перемещать файлы, получать их свойства и многое другое.

/*
Некоторые полезные методы и свойства класса FileInfo:

- CopyTo(path): копирует файл в новое место по указанному пути path

- Create(): создает файл

- Delete(): удаляет файл

- MoveTo(destFileName): перемещает файл в новое место

- Свойство Directory: получает родительский каталог в виде объекта DirectoryInfo

- Свойство DirectoryName: получает полный путь к родительскому каталогу

- Свойство Exists: указывает, существует ли файл

- Свойство Length: получает размер файла

- Свойство Extension: получает расширение файла

- Свойство Name: получает имя файла

- Свойство FullName: получает полное имя файла

*/

/*
Класс File реализует похожую функциональность с помощью статических методов:

- Copy(): копирует файл в новое место

- Create(): создает файл

- Delete(): удаляет файл

- Move: перемещает файл в новое место

- Exists(file): определяет, существует ли файл

*/

// 1. Получение информации о файле

string path = @"C:\SomeDir\htc.txt";
FileInfo fileInf = new Fileinfo(path);
if (fileInf.Exists)
{
	Console.WriteLine("Имя файла: {0}", fileInf.Name);
	Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
	Console.WriteLine("Размер: {0}", fileInf.Length);
}

// 2. Удаление файла

string path = @"C:\SomeDir\htc.txt";
FileInfo fi = new FileInfo(path);
if (fi.Exists)
	fi.Delete();

// альтернатива с помощью класса File:
File.Delete(path);

// стоит отметить такую особенность метода Delete(): если файла нет (но путь к нему верный), то ошибки не будет.


// 3. Перемещение файла

// создадим каталог, если его нет
string path = @"C:\SomeFloder";
if (!Directory.Exists(path))
	Directory.CreateDirectory(path);

string file = "C:\\SomeDir\\htc.txt";
string newFile = @"C:\SomeFloder\htc.txt";

FileInfo fi = newFileInfo(file);
if (fi.Exists)
	fi.MoveTo(newFile);

// альтернатива с помощью класса File
File.Move(file, newFile);

// 4. Копирование файла

string path = @"C:\SomeDir\htc.txt";
string newPath = "C:\\SomeFloder\\htc2.txt";

FileInfo fi = new FileInfo(path);
if (fi.Exists)
	fi.CopyTo(newPath, true)
// bool значение определяет, нужно ли перезаписывать файл

// альтернатива с помощью статистического класса File
File.Copy(path, newPath, true);

// Метод CopyTo класса FileInfo принимает два параметра: путь, по которому файл будет копироваться, и булевое значение, которое указывает, надо ли при копировании перезаписывать файл (если true, как в случае выше, файл при копировании перезаписывается). Если же в качестве последнего параметра передать значение false, то если такой файл уже существует, приложение выдаст ошибку.

// Метод Copy класса File принимает три параметра: путь к исходному файлу, путь, по которому файл будет копироваться, и булевое значение, указывающее, будет ли файл перезаписываться.


