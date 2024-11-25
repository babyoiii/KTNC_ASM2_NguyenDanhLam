using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tong
{
    internal class Tong
    {

        static void Main(string[] args)
        {
        }
        public Class tong;
        [SetUp]
        public void SetUp()
        {
            tong = new Class();
        }
        [Test]
        [TestCase(3, 6, 9)] 
        [TestCase(3.3f, 6.7f, 10)] // A và B float
        [TestCase(1, 2.2f, 3.2f)] // B float
        [TestCase(1.1f, 7, 8)] // A float
        [TestCase(0, 0, 0)] 
        [TestCase(float.MaxValue, float.MinValue, 1)] //Biên float
        [TestCase(int.MinValue, 1, -int.MaxValue)] //Biên int
        [TestCase(float.NaN, float.NaN, float.NaN)] // NaN
        [TestCase("Nguyen", "Danh", "Lam")] //String
        [TestCase("1", 0, 1)]
        public void TinhTong(float a, float b, float c)
        {
            var kq = tong.Tong(a, b);
            Assert.That(kq, Is.EqualTo(c));
        }
        public class Class
        {
            public float Tong(float a, float b) // Float
            {

                if (float.IsNaN(a) || float.IsNaN(b))
                {
                    throw new Exception("NaN");
                }
                if ((int)a != a || (int)b != b)
                {
                    throw new Exception("A hoặc B không phải là số nguyên");
                }
                return a + b;
            }
        }

    }
}

