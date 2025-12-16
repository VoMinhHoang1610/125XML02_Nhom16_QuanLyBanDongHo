using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuanLyBanDongHo.Model
{
    class KhachHang
    {
        XMLFile XmlFile = new XMLFile();
        public Boolean themKhachHang(String ten, int namSinh, String diaCHi) {
            try {
                XmlDocument XDoc = XmlFile.getXmlDocument("KhachHangs.xml");
                XmlElement node = XDoc.CreateElement("KhachHang");
                XmlElement maKH = XDoc.CreateElement("maKH");
                XmlElement tenKH = XDoc.CreateElement("tenKH");
                XmlElement namSinhKH = XDoc.CreateElement("namSinh");
                XmlElement soTien = XDoc.CreateElement("SoTienDaDung");
                XmlElement diaChikh = XDoc.CreateElement("diaChi");

                maKH.InnerText = taoMaKhachHang(XDoc);
                tenKH.InnerText = ten;
                soTien.InnerText = "0";
                namSinhKH.InnerText = namSinh.ToString();
                diaChikh.InnerText = diaCHi;

                node.AppendChild(maKH);
                node.AppendChild(tenKH);
                node.AppendChild(soTien);
                node.AppendChild(namSinhKH);
                node.AppendChild(diaChikh);

                XDoc.DocumentElement.AppendChild(node);


                XDoc.Save("KhachHangs.xml");
            }
            catch (Exception e) {

                Console.WriteLine(e.Message);
                return false;
            }



        return true;
        }

         String taoMaKhachHang(XmlDocument XDoc) {
     
            XmlNodeList temp = XDoc.SelectNodes("/KhachHangs/KhachHang[last()]");

            String maKH = temp[0].ChildNodes[0].InnerText;
            maKH = ("000000" + (int.Parse(maKH.Substring(2))+1).ToString());
            maKH = "KH"+maKH.Substring(maKH.Length - 5);
             return maKH;
        }

         public Boolean suaThongTin(String maKH,String ten,int namSinh,String diaCHi) {
             try {
                 XmlDocument XDoc = XmlFile.getXmlDocument("KhachHangs.xml");
                 XmlNodeList nodeList = XDoc.SelectNodes("/KhachHangs/KhachHang[maKH = '" + maKH + "']");
                 XmlNode node = nodeList[0];
                 node.ChildNodes[1].InnerText = ten;
                 node.ChildNodes[3].InnerText = namSinh.ToString();
                 node.ChildNodes[4].InnerText = diaCHi;

                 XDoc.Save("KhachHangs.xml");
             }
             catch {
                 return false;
             }

     

         return true;
         }

         public Boolean xoaThongTin(String maKH) {
             try {
                 XmlDocument Xdoc = XmlFile.getXmlDocument("KhachHangs.xml");
                 XmlNodeList nodeList = Xdoc.SelectNodes("/KhachHangs/KhachHang[maKH = '"+maKH+"']");
                 
                 if (nodeList.Count > 0)
                 {
                     Xdoc.DocumentElement.RemoveChild(nodeList[0]);
                     Xdoc.Save("KhachHangs.xml");
                 }
             }
             catch { 
                 return false;
             }
             return true;
         }





    }

    

}
