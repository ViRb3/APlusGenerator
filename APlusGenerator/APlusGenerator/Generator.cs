using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace APlusGenerator
{
    public class Student
    {
        public string FirstName, LastName, Class;

        public Student(string firstName, string lastName, string @class)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("Invalid first name!");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Invalid last name!");

            if (string.IsNullOrWhiteSpace(@class))
                throw new Exception("Invalid class!");

            FirstName = firstName;
            LastName = lastName;
            Class = @class;
        }

        public Student(string data)
        {
            string[] dataArray = Regex.Split(data, ":");

            if (dataArray.Length != 3)
                throw new Exception("Invalid FirstName:LastName:Class line!");

            FirstName = dataArray[0];
            LastName = dataArray[1];
            Class = dataArray[2];
        }

        public string GetData()
        {
            return string.Format("{0}:{1}:{2}", FirstName, LastName, Class);
        }

        public string GetEncryptedData()
        {
            string base64 = Encrypt(GetData());
            return base64;
        }

        private string Encrypt(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);

            using (AesManaged aes = new AesManaged())
            {
                aes.Key = GetCode();
                aes.GenerateIV();

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, 16);

                    using (var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(dataBytes, 0, dataBytes.Length);
                    }

                    byte[] encryptedBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        private byte[] GetCode()
        {
            int code = 0;

            foreach (char @char in "APlus")
                code += @char;

            return MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(code.ToString()));
        }
    }

    internal class Generator
    {
        private readonly List<Student> _students = new List<Student>();

        public Generator(Student student)
        {
            _students.Add(student);
        }

        public Generator(List<Student> students)
        {
            _students.AddRange(students);
        }

        public Tuple<Student, Bitmap[]>[] Generate(int count)
        {
            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            QrEncoder encoder = new QrEncoder();
            encoder.ErrorCorrectionLevel = ErrorCorrectionLevel.L;

            List<Tuple<Student, Bitmap[]>> results = new List<Tuple<Student, Bitmap[]>>();

            foreach (var student in _students)
            {
                List<Bitmap> codes = new List<Bitmap>();

                for (int i = 0; i < count; i++)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        renderer.WriteToStream(encoder.Encode(student.GetEncryptedData()).Matrix, ImageFormat.Png, memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        codes.Add(new Bitmap(memoryStream));
                    }
                }
                

                results.Add(new Tuple<Student, Bitmap[]>(student, codes.ToArray()));
            }

            return results.ToArray();
        }
    }
}