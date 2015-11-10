using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CompareFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            var dir = @"~\..\..\..\TestFiles\";
            string[] dirs = Directory.GetFiles(dir);
            Console.WriteLine("The number of files is {0}.", dirs.Length);
            int m = 0;
            for(int i = 0; i < dirs.Length; i++)
            {
                m = i+1;
                var file1 = dirs[i];
                for (int j = m; j < dirs.Length; j++)
                {
                    var file2 = dirs[j];
                    Console.WriteLine(Utils.FileCompare(file1, file2).ToString());
                }
            }
            Console.ReadLine();
        }
    }
    public static class Utils 
    {
        public static bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Открываем файлы
            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            Console.WriteLine("Compare files: \n{0} \n{1}", fs1.Name, fs2.Name);

            // Проверка длины файлов
            if (fs1.Length != fs2.Length)
            {
                //Закрываем файлы
                fs1.Close();
                fs2.Close();

                return false;
            }

            //Побитовое сравнивание
            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            //Закрываем файлы
            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
    }
}
