using System;
using System.Net.Sockets;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            ThuVien.NhanVien emp1 = new ThuVien.NhanVien();
            TcpClient client;

            try
            {
                client = new TcpClient("127.0.0.1", 9050);
            }
            catch (SocketException)
            {
                Console.WriteLine("Khong ket noi duoc voi server");
                return;
            }
            NetworkStream ns = client.GetStream();

            while (true)
            {
                emp1.NhapTuBanPhim();
                byte[] data = emp1.GetBytes(); // Chuyển đối tượng thành bytes
                int size = emp1.Size; // Kích thước của đối tượng
                Console.WriteLine("Kich thuoc goi tin = {0}", size);
                byte[] packsize = BitConverter.GetBytes(size);
                ns.Write(packsize, 0, 4); // Gửi kích thước của đối tượng/gói tin trước
                ns.Write(data, 0, size); // Sau đó mới gửi nội dung sau
                Console.Write("\n>> Ban co muon nhap tiep khong: ");
                if (Console.ReadLine().Equals("Khong", StringComparison.CurrentCultureIgnoreCase))
                {
                    ns.Write(new byte[4], 0, 4); // Gửi mảng rỗng
                    break;
                }
            }
            // Ngắt kết nối
            ns.Flush();
            ns.Close();
            client.Close();
            Console.ReadKey();
        }
    }
}
