using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hieu
{
    internal class Hieu
    {
        static void Main(string[] args)
        {
        }
        public Class hieu;

        [SetUp]
        public void SetUp()
        {
            hieu = new Class();
        }
        [Test]
        [TestCase(3, 2, 1)] // A lớn hơn B
        [TestCase(10, 10, 10)] // A bằng B
        [TestCase(2, 3, -1)] // A nhỏ hơn B
        [TestCase(0, 0, 0)] 
        [TestCase(-3, -4, 1)] // Âm
        [TestCase("HaHa", "Ha", "Ha")] 
        [TestCase(2.3f, 1.1f, 1.2f)] // Float
        [TestCase(int.MaxValue, 1, int.MaxValue -1)] //Biên
        [TestCase(int.MinValue, -1, -int.MaxValue)] 
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue)]
        public void TinhHieu(int a, int b, int c)
        {
            var kq = hieu.TinhHieu(a, b);
            Assert.That(kq, Is.EqualTo(c));
        }
        public class Class
        {
            public int TinhHieu(int a, int b) //Int
            {  
                return a - b;
            }
        }
    }
}