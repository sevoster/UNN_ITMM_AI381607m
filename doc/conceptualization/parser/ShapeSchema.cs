﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// Этот исходный код был создан с помощью xsd, версия=4.6.1055.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Point {
    
    private double xField;
    
    private double yField;
    
    /// <remarks/>
    public double X {
        get {
            return this.xField;
        }
        set {
            this.xField = value;
        }
    }
    
    /// <remarks/>
    public double Y {
        get {
            return this.yField;
        }
        set {
            this.yField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class BezierCurve {
    
    private Point[] pointField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Point")]
    public Point[] Point {
        get {
            return this.pointField;
        }
        set {
            this.pointField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Shape2D {
    
    private Contour[] contourField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Contour")]
    public Contour[] Contour {
        get {
            return this.contourField;
        }
        set {
            this.contourField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class Contour {
    
    private Point[] pointsField;
    
    private Point[][] segmentsField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("Point", IsNullable=false)]
    public Point[] Points {
        get {
            return this.pointsField;
        }
        set {
            this.pointsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("BezierCurve", IsNullable=false)]
    [System.Xml.Serialization.XmlArrayItemAttribute("Point", IsNullable=false, NestingLevel=1)]
    public Point[][] Segments {
        get {
            return this.segmentsField;
        }
        set {
            this.segmentsField = value;
        }
    }
}
