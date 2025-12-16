using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuanLyBanDongHo.Model
{
    class ChiTietSanPham
    {

        XMLFile XmlFile = new XMLFile();
        public XmlNodeList getListName() {
            
            XmlDocument XDoc = XmlFile.getXmlDocument("ChiTietSanPhams.xml");

            return XDoc.SelectNodes("/ChiTietSanPhams/ChiTietSanPham");
        }

        public void setSoluong(int soLuongTraoDoi,XmlNode node) {
            if(node != null)
            node.ChildNodes[2].InnerText = (int.Parse(node.ChildNodes[2].InnerText)+ soLuongTraoDoi).ToString();
        }

        public void updateGiaNhap(int giaNhapMoi, XmlNode node) {
            if(node != null && node.ChildNodes.Count > 5)
            {
                // Cập nhật giá nhập (ChildNodes[5])
                node.ChildNodes[5].InnerText = giaNhapMoi.ToString();
            }
        }

    }
}
