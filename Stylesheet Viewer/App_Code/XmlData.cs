using System;
using System.Xml;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;



public class XmlData
{
    XmlNode PrntClumnHeadNode = null;
    XmlNode _ClumnHeadNode     = null;
    string _ColumnHead ="";
    string _Column1    = "";
    string _Column2    = "";
    string _Column3    = "";
    string _Column4    = "";
    string _Column5    = "";
    bool   _onlyHead   = false;
    bool   _PIT        = false;
  

    #region constructor
    public XmlData(XmlNode ColumnHeadNode, string Column1, string Column2, string Column3, string Column4, string Column5)
    {
        _ClumnHeadNode = ColumnHeadNode;
        _ColumnHead = ColumnHeadNode.Name.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column2 = Column2.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column3 = Column3.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column4 = Column4.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column5 = Column5.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&"); 
    }
    public XmlData(XmlNode ColumnHeadNode, string Column1, string Column2, string Column3, string Column4)
    {
        _ClumnHeadNode = ColumnHeadNode;
        _ColumnHead = ColumnHeadNode.Name.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column2 = Column2.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column3 = Column3.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column4 = Column4.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&"); 
    }
    public XmlData(XmlNode ColumnHeadNode, string Column1, string Column2, string Column3)
    {
        _ClumnHeadNode = ColumnHeadNode;
        _ColumnHead = ColumnHeadNode.Name.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column2 = Column2.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column3 = Column3.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&"); 
    }
    public XmlData(XmlNode ColumnHeadNode, string Column1, string Column2)
    {
        _ClumnHeadNode = ColumnHeadNode;
        _ColumnHead = ColumnHeadNode.Name.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&"); 
        _Column2 = Column2.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&"); 
    }
    public XmlData(XmlNode ColumnHeadNode, string Column1)
    {
        _ClumnHeadNode = ColumnHeadNode;
        _ColumnHead = ColumnHeadNode.Name.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
    }
    public XmlData(XmlNode ColumnHeadNode, bool onlyHead)
    {
        _ClumnHeadNode = ColumnHeadNode;
        _ColumnHead = ColumnHeadNode.Name.Replace("#$#", "&");
        _onlyHead = onlyHead;
    }

    public XmlData(string ColumnHead, string Column1, string Column2)
    {
        _ColumnHead = ColumnHead.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        _Column2 = Column2.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
    }
    public XmlData(string ColumnHead, string Column1)
    {
        _ColumnHead = ColumnHead.Replace("#$#", "&");
        _Column1 = Column1.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
    }
    public XmlData(string ColumnHead, bool onlyHead)
    {
        _ColumnHead = ColumnHead.Replace("#$#", "&");
        _onlyHead = onlyHead;
    }
    
    #endregion
    public bool OnlyHead
    {
        get
        {
            return _onlyHead;
        }

    }
    public bool PIT
    {
        get
        {
            return _PIT;
        }
        set
        {
            _PIT = value;
        }

    }
    public string getColumnHead()
    {
        return _ColumnHead; 
    }
    #region Public propierties
    public string ColumnHead
    {
        
        get 
        {


            if ("multipleVersion".Equals(_ColumnHead))
            {
            }
            if( _ColumnHead.Equals(""))
                return _ColumnHead; 

            string colHead = GloVar.ElementDescObj.GetElementDespCription(_ColumnHead);

            if (_ClumnHeadNode != null)
            {
                string XSLColHead = GloVar.GetColHeadDescription(_ClumnHeadNode.Name, _ClumnHeadNode.ParentNode.Name, _Column1);

                if (string.IsNullOrEmpty(XSLColHead))
                    System.Diagnostics.Debug.Print("AAA :: " + _ColumnHead);
                else
                    colHead = XSLColHead;
            }
            else
            {
                string XSLColHead = GloVar.GetHeadDescription(_ColumnHead, _Column1);

                if (string.IsNullOrEmpty(XSLColHead))
                    System.Diagnostics.Debug.Print("AAA :: " + _ColumnHead);
                else
                    colHead = XSLColHead;
            }


            if ("NotDefine".Equals(_ColumnHead))
            {
            }

            if (!colHead.Equals("") || _ColumnHead.Equals("copyPermDiscl"))
            {
                return colHead;
            }
            else
                return _ColumnHead;
        }
    }
    public string Column1
    {
        get
        {
            if (_ColumnHead.Equals("journalSpecificCover"))
            { 
            }

            //return _Column2;
            if (_ClumnHeadNode!=null &&  "012345".IndexOf(_Column1) != -1 && !_Column1.Equals(""))
            {
                //string AtrValue = GloVar.ElementDescObj.GetElementDespCription(_ColumnHead, _Column1);
                string AtrValue = GloVar.GetElementDescription(_ClumnHeadNode.Name, _ClumnHeadNode.ParentNode.Name, _Column1);
                return AtrValue;
            }
            else if ( "012345".IndexOf(_Column1) != -1 && !_Column1.Equals(""))
            {
                string AtrValue = GloVar.GetElementDescriptionWithoutPrntNode(_ColumnHead, _Column1);
                return AtrValue;
            }
            else
            {
                return _Column1;
            }
        }
        set
        {
            _Column1 = value.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        }
    }
    public string Column2
    {
        get 
        {
            //return _Column2;
            if (_ClumnHeadNode != null && "012345".IndexOf(_Column2) != -1 && !_Column2.Equals(""))
            {
                //string AtrValue = GloVar.ElementDescObj.GetElementDespCription(_ColumnHead, _Column2);
                string AtrValue = GloVar.GetElementDescription(_ClumnHeadNode.Name, _ClumnHeadNode.ParentNode.Name, _Column1);
                return AtrValue;
            }
            else if ("012345".IndexOf(_Column1) != -1 && !_Column1.Equals(""))
            {
                string AtrValue = GloVar.GetElementDescriptionWithoutPrntNode(_ColumnHead, _Column1);
                return AtrValue;
            }

            else
            {
                return _Column2;
            }
        }
        set
        {
            _Column2 = value.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        }
    }
    public string Column3
    {
        get
        {
            //return _Column2;
            if (_ClumnHeadNode != null && "012345".IndexOf(_Column3) != -1 && !_Column3.Equals(""))
            {
                //string AtrValue = GloVar.ElementDescObj.GetElementDespCription(_ColumnHead, _Column3);
                string AtrValue = GloVar.GetElementDescription(_ClumnHeadNode.Name, _ClumnHeadNode.ParentNode.Name, _Column1);
                return AtrValue;
            }
            else if ("012345".IndexOf(_Column1) != -1 && !_Column1.Equals(""))
            {
                string AtrValue = GloVar.GetElementDescriptionWithoutPrntNode(_ColumnHead, _Column1);
                return AtrValue;
            }

            else
            {
                return _Column3;
            }
        }
        set
        {
            _Column3 = value.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        }
    }
    public string Column4
    {
        get
        {
            //return _Column2;
            if (_ClumnHeadNode != null && "012345".IndexOf(_Column4) != -1 && !_Column4.Equals(""))
            {
                //string AtrValue = GloVar.ElementDescObj.GetElementDespCription(_ColumnHead, _Column4);
                string AtrValue = GloVar.GetElementDescription(_ClumnHeadNode.Name, _ClumnHeadNode.ParentNode.Name, _Column1);
                return AtrValue;
            }
            else if ("012345".IndexOf(_Column1) != -1 && !_Column1.Equals(""))
            {
                string AtrValue = GloVar.GetElementDescriptionWithoutPrntNode(_ColumnHead, _Column1);
                return AtrValue;
            }

            else
            {
                return _Column4;
            }
        }
        set
        {
            _Column4 = value.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        }
    }
    public string Column5
    {
        get
        {
            //return _Column2;
            if (_ClumnHeadNode != null && "012345".IndexOf(_Column5) != -1 && !_Column5.Equals(""))
            {
                string AtrValue = GloVar.ElementDescObj.GetElementDespCription(_ColumnHead, _Column5);
                string AtrValue1 = GloVar.GetElementDescription(_ClumnHeadNode.Name, _ClumnHeadNode.ParentNode.Name, _Column1);
                return AtrValue;
            }
            else
            {
                return _Column5;
            }
        }
        set
        {
            _Column5 = value.Trim(new char[] { ' ', '\n' }).Replace("#$#", "&");
        }
    }

    #endregion
}

