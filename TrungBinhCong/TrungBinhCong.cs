using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrungBinhCong
{
    internal class TrungBinhCong
    {
        static void Main(string[] args)
        {
        }
        public Class tbc;
        [SetUp]
        public void SetUp()
        {
            tbc = new Class();
        }
        [Test]
        [TestCase(new double[] { 1, 2, 3 }, 2)]
        [TestCase(new double[] { 10, 20, 30 }, 20)] 
        [TestCase(new double[] { -1, -2, -3 }, -2)] //Âm
        [TestCase(new double[] { 1.5, 2.5, 3.5 }, 2.5)] // Double
        [TestCase(new double[] { 0, 0, 0 }, 0)]
        [TestCase(new double[] { 5 }, 5)] // 1 phần tử
        [TestCase(new double[] {}, 0)] //Rỗng
        [TestCase(new double[] { double.MaxValue, double.MaxValue }, double.MaxValue)] //Biên
        [TestCase(new double[] { 0.000001, 0.000002, 0.000003 }, 0.000002)] //Float
        [TestCase(new double[] { double.NaN, double.NaN , 3}, 1)] // NaN
        public void TBC(double[] a, double b)
        {
            var kq = tbc.TrungBinhCong(a);
            Assert.That(kq, Is.EqualTo(b));
        }
        public class Class
        {
            public double TrungBinhCong(double[] a)
            {
                if (a.Length == 0)
                {
                    throw new Exception("Mảng rỗng");
                }
                double tong = 0;
                for (int i = 0; i < a.Length; i++)
                {
                    tong += a[i];
                }
                double ketqua = tong / a.Length;
                return ketqua;
            }
        }
    }
}
