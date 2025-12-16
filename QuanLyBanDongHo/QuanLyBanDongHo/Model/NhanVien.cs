using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuanLyBanDongHo.Model
{
    class NhanVien
    {
        XMLFile XmlFile = new XMLFile();

        public XmlNodeList getListNhanVien()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("NhanViens.xml");
            return XDoc.SelectNodes("/NhanViens/NhanVien");
        }

        public bool kiemTraDangNhap(string tenDN, string matKhau)
        {
            try
            {
                XmlDocument XDoc = XmlFile.getXmlDocument("NhanViens.xml");
                XmlNodeList nodeList = XDoc.SelectNodes("/NhanViens/NhanVien[tenDN='" + tenDN + "' and matKhau='" + matKhau + "']");
                return nodeList.Count > 0;
            }
            catch
            {
                return false;
            }
        }

        public string getMaNVByTenDN(string tenDN)
        {
            try
            {
                XmlDocument XDoc = XmlFile.getXmlDocument("NhanViens.xml");
                XmlNodeList nodeList = XDoc.SelectNodes("/NhanViens/NhanVien[tenDN='" + tenDN + "']");
                if (nodeList.Count > 0)
                    return nodeList[0].ChildNodes[0].InnerText;
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
