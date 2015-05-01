using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;

namespace APlusGenerator
{
    public class Student
    {
        public string EMail, FirstName, LastName, Class;

        public Student(string email, string firstName, string lastName, string @class)
        {
            if (!RegexUtilities.IsValidEmail(email))
                throw new Exception("Invalid E-mail!");

            if (string.IsNullOrWhiteSpace(firstName))
                throw new Exception("Invalid first name!");

            if (string.IsNullOrWhiteSpace(lastName))
                throw new Exception("Invalid last name!");

            if (string.IsNullOrWhiteSpace(@class))
                throw new Exception("Invalid class!");

            EMail = email;
            FirstName = firstName;
            LastName = lastName;
            Class = @class;
        }

        public string GetData()
        {
            return EMail;
        }

        public string GetEncryptedData()
        {
            string base64 = Encrypt(GetData());
            return base64;
        }

        private string Encrypt(string data)
        {
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            const string key = @"v0,|m4Q/9K9mN'z*{RGL0@7eL2R8pHq4";

            using (var rijndael = new RijndaelManaged())
            {
                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.BlockSize = 256;
                rijndael.KeySize = 256;
                rijndael.GenerateIV();

                rijndael.Key = Encoding.UTF8.GetBytes(key);

                using (var memoryStream = new MemoryStream())
                using (var cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cryptoStream.Write(dataBytes, 0, dataBytes.Length);
                    cryptoStream.FlushFinalBlock();

                    memoryStream.Write(rijndael.IV, 0, rijndael.IV.Length);

                    data = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            return data;
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
            var renderer = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black,
                Brushes.White);
            var encoder = new QrEncoder();
            encoder.ErrorCorrectionLevel = ErrorCorrectionLevel.L;

            var results = new List<Tuple<Student, Bitmap[]>>();

            foreach (Student student in _students)
            {
                var codes = new List<Bitmap>();

                for (var i = 0; i < count; i++)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        renderer.WriteToStream(encoder.Encode(student.GetEncryptedData()).Matrix, ImageFormat.Png,
                            memoryStream);
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