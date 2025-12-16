using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuanLyBanDongHo.Model
{
    class ChiTietHoaDon
    {
        XMLFile XmlFile = new XMLFile();

        public XmlNodeList getListChiTietHoaDon()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("ChiTietHoaDons.xml");
            return XDoc.SelectNodes("/ChiTietHoaDons/ChiTietHoaDon");
        }

        public XmlNodeList getChiTietByMaHD(string maHD)
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("ChiTietHoaDons.xml");
            return XDoc.SelectNodes("/ChiTietHoaDons/ChiTietHoaDon[maHD='" + maHD + "']");
        }
    }
}
