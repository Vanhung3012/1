using System;
using System.Net;
using System.Net.Sockets;
using ThuVien;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            byte[] data = new byte[1024];
            TcpListener server = new TcpListener(IPAddress.Any, 9050);
            server.Start();
            TcpClient client = server.AcceptTcpClient();
            NetworkStream ns = client.GetStream();

            while (true)
            {
                byte[] size = new byte[4];
                int recv = ns.Read(size, 0, 4); 
                int packsize = BitConverter.ToInt32(size, 0);// Nhận kích thước gói tin từ client gửi lên trước
                if (packsize == 0) // Nếu như kích thước = 0 thì thoát
                    break;
                Console.WriteLine("Kich thuoc goi tin = {0}", packsize);
                recv = ns.Read(data, 0, packsize); // Nhận mảng bytes chứa dữ liệu đối tượng sau
                NhanVien emp1 = new NhanVien(data); // Chuyển bytes thành đối tượng
                Console.WriteLine(emp1);
            }
            // Ngắt kết nối
            ns.Close();
            client.Close();
            server.Stop();
            Console.WriteLine("Ket thuc nhan du lieu");
            Console.ReadKey();
        }
    }
}
