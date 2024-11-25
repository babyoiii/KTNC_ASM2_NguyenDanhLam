using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static CRUD_SinhVien.CRUD_SinhVien;

namespace CRUD_SinhVien
{
    internal class CRUD_SinhVien
    {
        static void Main(string[] args)
        {
        }
        public SinhVien _sv;
        public SinhVienPoly _poly;
        [SetUp]
        public void SetUp()
        {
            _poly = new SinhVienPoly();
        }
        [Test] //Test Thêm
        [TestCase("2", "PH00001", "Hehe", "SD19204", "SOF304")]
        [TestCase("3", "PH47854", "Haha", "SD19204", "SOF304")] //Trùng MaSV
        [TestCase("4", "PH0002", 8, 9, 10)] //Thông tin không hợp lệ
        [TestCase("5", "01", "Danh Lâm", "SD19204", "SOF304")] //Không chứa PH
        [TestCase("6", "PH47854", "", "", "")] //Rỗng
        [TestCase("7", "PH0003", "KiemThu", "SD19204", "A123@")] // Tenlop chứa kí tự đặc biệt
        public void ThemSinhVien(string id, string masv, string hoten, string malop, string tenlop)
        {
            _sv = new SinhVien("1", "PH47854", "Danh Lâm", "SD19204", "SOF304");
            _poly.Them(_sv);
            _sv = new SinhVien(id, masv, hoten, malop, tenlop);
            _poly.Them(_sv);
            Assert.That(_poly.DanhSach().Contains(_sv), Is.True);
        }
        [Test] //Test Sửa
        [TestCase("1", "PH00001", "Hehe", "SD19204", "SOF304")]
        [TestCase("2", "PH47854", "Danh Lâm", "SD19204", "SOF304")]// Không tìm thấy ID
        public void SuaSinhVien(string id, string masv, string hoten, string malop, string tenlop)
        {
            _sv = new SinhVien("1", "PH47854", "Danh Lâm", "SD19204", "SOF304");
            _poly.Them(_sv);
            var suaSV = new SinhVien(id, masv, hoten, malop, tenlop);
            _poly.Sua(id, suaSV);
            Assert.That(_poly.DanhSach().Contains(_sv), Is.True);
        }
        [Test] //Test Xóa
        [TestCase("1")]
        [TestCase("2")] //Không tìm thấy ID
        public void XoaSinhVien(string id)
        {
            _sv = new SinhVien("1", "PH47854", "Danh Lâm", "SD19204", "SOF304");
            _poly.Them(_sv);
            _poly.Xoa(id);
            Assert.That(_poly.DanhSach().Contains(_sv), Is.False);
        }
        [Test] //Test Tìm kiếm
        [TestCase("PH47854", new string[] {"PH47854" })]
        [TestCase("PH", new string[] { "PH47854", "PH00001", "PH00002" })] //Toàn bộ danh sách MaSV chứa PH
        [TestCase("", new string[] { "PH47854", "PH00001", "PH00002" })] // Tìm kiếm rỗng sẽ ra danh sách
        [TestCase("    ", new string[] { "PH47854", "PH00001", "PH00002" })] //Space
        [TestCase("", new string[] { "PH47854", "PH00001" })] // Failed do thiếu PH00002
        [TestCase("DanhLam", new string[] {})] // Passed vì không tìm thấy đối tượng nào
        [TestCase("1", new string[] {"PH00001" })] // Tìm số
        [TestCase("2", new string[] { "PH00001" })] // Failed
        public void TimKiem_MaSV(string masv, string[] check)
        {
            _sv = new SinhVien("1", "PH47854", "Danh Lâm", "SD19204", "SOF304");
            _poly.Them(_sv);
            _sv = new SinhVien("2", "PH00001", "Hehe", "SD19204", "SOF304");
            _poly.Them(_sv);
            _sv = new SinhVien("3", "PH00002", "Haha", "SD19204", "SOF304");
            _poly.Them(_sv);
            var kq = _poly.TimKiem(masv).Select(x => x.MaSV).ToArray();
            Assert.That(kq, Is.EqualTo(check));
        }
        public class SinhVien
        {
            public string ID { get; set; }
            public string MaSV { get; set; }
            public string HoTen { get; set; }
            public string MaLop { get; set; }
            public string TenLop { get; set; }
            public SinhVien(string id, string masv, string hoten, string malop, string tenlop)
            {
                ID = id;
                MaSV = masv;
                HoTen = hoten;
                MaLop = malop;
                TenLop = tenlop;
            }
        }
        public class SinhVienPoly
        {
            List<SinhVien> sinhViens = new List<SinhVien>();
            public void Them(SinhVien sv)
            {

                if (string.IsNullOrWhiteSpace(sv.ID) || string.IsNullOrWhiteSpace(sv.HoTen) || string.IsNullOrWhiteSpace(sv.MaLop) || string.IsNullOrWhiteSpace(sv.TenLop))
                {
                    throw new Exception("Thông tin sinh viên không hợp lệ.");
                }
                if (string.IsNullOrWhiteSpace(sv.MaSV) || !sv.MaSV.StartsWith("PH"))
                {
                    throw new Exception("MaSV phải bắt đầu bằng 'PH'.");
                }
                if (sinhViens.Any(e => e.MaSV == sv.MaSV))
                {
                    throw new Exception("MaSV phải là duy nhất.");
                }
                if (!Regex.IsMatch(sv.TenLop, @"^[a-zA-Z0-9\s]+$"))
                {
                    throw new Exception("TenLop không được chứa ký tự đặc biệt.");
                }
                sinhViens.Add(sv);
            }

            public void Sua(string id, SinhVien sv)
            {
                var suaSV = sinhViens.Find(x => x.ID == id);
                if (suaSV == null)
                {
                    throw new Exception($"Không tìm thấy sinh viên có ID {id}");
                }
                suaSV.ID = sv.ID;
                suaSV.MaSV = sv.MaSV;
                suaSV.HoTen = sv.HoTen;
                suaSV.MaLop = sv.MaLop;
                suaSV.TenLop = sv.TenLop;
            }

            public void Xoa(string id)
            {
                var xoaSV = sinhViens.Find(x => x.ID == id);
                if (xoaSV == null)
                {
                    throw new Exception($"Không tìm thấy sinh viên có ID {id}");
                }
                sinhViens.Remove(xoaSV);
            }
            public List<SinhVien> TimKiem(string masv)
            {
                if (string.IsNullOrWhiteSpace(masv))
                {
                    return sinhViens;
                }
                var svcantim = sinhViens.Where(x => x.MaSV.Contains(masv)).ToList();
                return svcantim;
            }
            public List<SinhVien> DanhSach()
            {
                return sinhViens;
            }
        }
    }
}
