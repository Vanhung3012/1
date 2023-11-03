using System;
using System.Text;

namespace ThuVien
{
    public class NhanVien
    {
        public uint MaNV;
        public string HoTen;
        public string NgaySinh;
        public double HeSoLuong;
        public int Size; // Kich thuoc goi tin

        public NhanVien() { }

        public NhanVien(byte[] data) // CHuyển byte thành đối tượng
        {
            int place = 0;
            MaNV = BitConverter.ToUInt32(data, place);
            place += 4;
            int hoTenSize = BitConverter.ToInt32(data, place);
            place += 4;
            HoTen = Encoding.UTF8.GetString(data, place, hoTenSize);
            place += hoTenSize;
            int ngaySinhSize = BitConverter.ToInt32(data, place);
            place += 4;
            NgaySinh = Encoding.UTF8.GetString(data, place, ngaySinhSize);
            place += ngaySinhSize;
            HeSoLuong = BitConverter.ToDouble(data, place);
            place += 8;
            Size = place;
        }


        public byte[] GetBytes() // chuyển đối tượng thành byte
        {
            byte[] data = new byte[1024];
            int place = 0;
            Buffer.BlockCopy(BitConverter.GetBytes(MaNV), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(BitConverter.GetBytes(HoTen.Length), 0, data, place, 4);//Gửi kích thước chuỗi họ tên
            place += 4;
            Buffer.BlockCopy(Encoding.UTF8.GetBytes(HoTen), 0, data, place, HoTen.Length); // Gửi nội dung chuỗi họ tên
            place += HoTen.Length;
            Buffer.BlockCopy(BitConverter.GetBytes(NgaySinh.Length), 0, data, place, 4);
            place += 4;
            Buffer.BlockCopy(Encoding.UTF8.GetBytes(NgaySinh), 0, data, place, NgaySinh.Length);
            place += NgaySinh.Length;
            Buffer.BlockCopy(BitConverter.GetBytes(HeSoLuong), 0, data, place, 8);
            place += 8;
            Size = place;
            return data;
        }

        public void NhapTuBanPhim()
        {
            Console.Write("Nhap ma nhan vien: ");
            MaNV = uint.Parse(Console.ReadLine());
            Console.Write("Nhap ho va ten: ");
            HoTen = Console.ReadLine();
            Console.Write("Nhap Ngay Sinh: ");
            NgaySinh = Console.ReadLine();
            Console.Write("Nhap He So Luong: ");
            HeSoLuong = double.Parse(Console.ReadLine());
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Ma Nhan Vien: {0} ", MaNV));
            sb.AppendLine(String.Format("Ho va ten: {0}", HoTen));
            sb.AppendLine(String.Format("Ngay Sinh: {0}", NgaySinh));
            sb.AppendLine(String.Format("He so luong: {0}", HeSoLuong));
            sb.AppendLine(String.Format("Kich thuoc goi: {0}", Size));
            return sb.ToString();
        }
    }
}
