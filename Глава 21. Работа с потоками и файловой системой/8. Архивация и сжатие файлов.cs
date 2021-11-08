// Кроме классов чтения-записи .NET предоставляет классы, которые позволяют сжимать файлы, а также затем восстанавливать их в исходное состояние.

// Это классы ZipFile, DeflateStream и GZipStream, которые находятся в пространстве имен System.IO.Compression и представляют реализацию одного из алгоритмов сжатия Deflate или GZip.

// 											GZipStream и DeflateStream
/*
Для создания объекта GZipStream можно использовать один из его конструкторов:

1. GZipStream(Stream stream, CompressionLevel level): stream представляет данные, а level задает уровень сжатия

2. GZipStream(Stream stream, CompressionMode mode): mode указывает, будут ли данные сжиматься или, наоборот, восстанавливаться и может принимать два значения:

2.1. CompressionMode.Compress: данные сжимаются

2.2. CompressionMode.Decompress: данные восстанавливатьсяся

Если данные сжимаются, то stream указывает на поток архивируемых данных. Если данные восстанавливаются, то stream указывает на поток, куда будут передаваться восстановленные данные.

3. GZipStream(Stream stream, CompressionLevel level, bool leaveMode): параметр leaveMode указывает, надо ли оставить открытым поток stream после удаления объекта GZipStream. Если значение true, то поток остается открытым

4. GZipStream(Stream stream, CompressionMode mode, bool leaveMode)
Для управления сжатием/восстанавлением данных GZipStream предоставляет ряд методов. Основые из них:

- void CopyTo(Stream destination): копирует все данные в поток destination

- Task CopyToAsync(Stream destination): асинхронная версия метода CopyTo

- void Flush(): очищает буфер, записывая все его данные в файл

- Task FlushAsync(): асинхронная версия метода Flush

- int Read(byte[] array, int offset, int count): считывает данные из файла в массив байтов и возвращает количество успешно считанных байтов. Принимает три параметра:

1. array - массив байтов, куда будут помещены считываемые из файла данные

2. offset представляет смещение в байтах в массиве array, в который считанные байты будут помещены

3. count - максимальное число байтов, предназначенных для чтения. Если в файле находится меньшее количество байтов, то все они будут считаны.

- Task<int> ReadAsync(byte[] array, int offset, int count): асинхронная версия метода Read

- long Seek(long offset, SeekOrigin origin): устанавливает позицию в потоке со смещением на количество байт, указанных в параметре offset.

- void Write(byte[] array, int offset, int count): записывает в файл данные из массива байтов. Принимает три параметра:

1. array - массив байтов, откуда данные будут записываться в файл

2. offset - смещение в байтах в массиве array, откуда начинается запись байтов в поток

3. count - максимальное число байтов, предназначенных для записи

- Task WriteAsync(byte[] array, int offset, int count): асинхронная версия метода Write
*/

using System;
using System.IO;
using System.IO.Compression;

class Program
{
    public static void Compress(string sourceFile, string compressedFile)
    {
        // поток для чтения исходного файла
        using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
        {
            // поток для записи сжатого файла
            using (FileStream targetStream = File.Create(compressedFile))
            {
                // поток архивации
                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    sourceStream.CopyTo(compressionStream);  // копируем байты из одного потока в другой, тем самым сжимая файл
                    Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}\tСжатый размер: {2}.",
                    sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                }
            }
        }
    }

    public static void Decompress(string compressedFile, string targetFile)
    {
        // поток для чтения из сжатого файла
        using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
        {
            // поток для записи восстанавленного файла
            using (FileStream targetStream = File.Create(targetFile))
            {
                // поток разархивации
                using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(targetStream);   // передаем данные в поток восстанавливаемого файла, тем самым восстанавливая файл
                    Console.WriteLine("Восстановлен файл: {0}", targetFile);
                }
            }
        }
    }
    static void Main()
    {
        string sourceFile = @"C:\SomeFolder\kniga.pdf";     // исходный файл
        string compressedFile = @"C:\SomeFolder\kniga.gz";  // сжатый файл
        string targetFile = @"C:\SomeFolder\kniga_ungzip.pdf";  // восстановленный файл

        // создание сжатого файла
        Compress(sourceFile, compressedFile);
        // восстановление файла
        Decompress(compressedFile, targetFile);
    }
}

// Метод Compress получает название исходного файла, который надо архивировать, и название будущего сжатого файла.

// Сначала создается поток для чтения из исходного файла - FileStream sourceStream. Затем создается поток для записи в сжатый файл - FileStream targetStream. Поток архивации GZipStream compressionStream инициализируется потоком targetStream и с помощью метода CopyTo() получает данные от потока sourceStream.

// Метод Decompress производит обратную операцию по восстановлению сжатого файла в исходное состояние. Он принимает в качестве параметров пути к сжатому файлу и будущему восстановленному файлу.

// Здесь в начале создается поток для чтения из сжатого файла FileStream sourceStream, затем поток для записи в восстанавливаемый файл FileStream targetStream. В конце создается поток GZipStream decompressionStream, который с помощью метода CopyTo() копирует восстановленные данные в поток targetStream.

// Чтобы указать потоку GZipStream, для чего именно он предназначен - сжатия или восстановления - ему в конструктор передается параметр CompressionMode, принимающий два значения: Compress и Decompress.

// !!! Если бы захотели бы использовать другой класс сжатия - DeflateStream, то мы могли бы просто заменить в коде упоминания GZipStream на DeflateStream, без изменения остального кода. Их использование идентично.

// В то же время при использовании этих классов есть некоторые ограничения, в частности, мы можем сжимать только один файл. Для архивации группы файлов лучше выбрать другие инструменты, например, ZipFile.

// 												ZipFile
/* 
Статический класс ZipFile из простанства имен System.IO.Compression предоставляет дополнительные возможности для создания архивов. Он позволяет создавать архив из каталогов. Его основные методы:

- void CreateFromDirectory(string sourceDirectoryName, string destinationFileName): архивирует папку по пути sourceDirectoryName в файл с названием destinationFileName

- void ExtractFromDirectory(string sourceFileName, string destinationDirectoryName): извлекает все файлы из zip-файла sourceFileName в каталог destinationDirectoryName
*/

// Оба метода имеют ряд дополнительных перегруженных версий. Рассмотрим их применение:

using System;
using SystemIO;
using System.IO.Compression;

namespace CompressApp
{
	class Program
	{
		static void Main()
		{
			string sourceFolder = @"C:\SomeFolder";	// исходная папка
			string zipFile = @"C:\SomeFolder.zip";	// сжатый файл
			string targetFolder = @"C:\SomeNewFolder";	// папка, куда будет распаковываться файл
			
			ZipFile.CreateFromDirectory(sourceFolder, zipFile);
			Console.WriteLine($"Папка {sourceFolder} архивированна в файл {zipFile}");
			
			ZipFile.ExtractToDirectory(zipFile, targetFolder);
			Console.WriteLine($"Файл {zipFile} распакован в папку {targetFolder}");
            Console.ReadLine();
		}
	}
}

// В данном случае папка "C:\SomeFolder" методом ZipFile.CreateFromDirectory архивируется в файл SomeFolder.zip. Затем метод ZipFile.ExtractToDirectory() распаковывает данный файл в папку "C:\SomeNewFolder" (если такой папки нет, она создается).
















