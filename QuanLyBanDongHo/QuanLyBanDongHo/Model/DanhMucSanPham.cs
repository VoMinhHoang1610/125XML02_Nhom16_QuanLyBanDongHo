using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuanLyBanDongHo.Model
{
    class DanhMucSanPham
    {

        XMLFile XmlFile = new XMLFile();
        public XmlNodeList getListMD()
        {
            XmlDocument XDoc = XmlFile.getXmlDocument("DanhMucSanPhams.xml");
            XmlNodeList nodeList = XDoc.SelectNodes("/DanhMucSanPhams/DanhMucSanPham");
            for (int i = 0; i < nodeList.Count; i++) {
                XmlNode a = nodeList[i];
  

            }
                return nodeList;

        }
    }
}
