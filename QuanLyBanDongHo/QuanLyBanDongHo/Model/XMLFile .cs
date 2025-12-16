using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace QuanLyBanDongHo.Model
{
    class XMLFile
    {
      
        //get XmlDocument 
        public XmlDocument getXmlDocument(String fileName) {
            XmlDocument Xd = new XmlDocument();
            Xd.Load(fileName);
            return Xd;
        }
        public Boolean themHoaDon(XmlDocument XDoc,String maHD,String maNV, String maKH,String loai) {
            try {
                XmlElement el = XDoc.CreateElement("HoaDonNhapXuat");
                XmlElement mHD = XDoc.CreateElement("maHD");
                mHD.InnerText = maHD;

                XmlElement mNV = XDoc.CreateElement("maNV");
                mNV.InnerText = maNV;

                XmlElement mKH = XDoc.CreateElement("maKH");
                mKH.InnerText = maKH;

                XmlElement l = XDoc.CreateElement("LoaiHD");
                l.InnerText = loai;

                el.AppendChild(mHD);
                el.AppendChild(mNV);
                el.AppendChild(mKH);
                el.AppendChild(l);
                XDoc.DocumentElement.AppendChild(el);
                XDoc.Save("HoaDonNhapXuats.xml");






            }
            catch { return false; }


            return true;
        }

    }
}
