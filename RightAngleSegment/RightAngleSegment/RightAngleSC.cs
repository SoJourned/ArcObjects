using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geometry;
using System;

namespace RightAngleSegment
{

  class RightAngleSC : StraightConstructorClass, IPersist
  {
    private IEditor m_editor;
    private IEditSketch3 m_EdSketch;

    public override void Activate()
    {
      base.Activate();
    }

    public override bool Active
    {
      get
      {
        return base.Active;
      }
    }
    
    public override ESRI.ArcGIS.Geometry.IPoint Anchor
    {
      get
      {
        return base.Anchor;
      }
    }

    public override double AngleConstraint
    {
      get
      {
        return base.AngleConstraint;
      }
      set
      {
        base.AngleConstraint = value;
      }
    }

    public override esriSketchConstraint Constraint
    {
      get
      {
        return base.Constraint;
      }
      set
      {
        base.Constraint = value;
      }
    }


    public override void AddPoint(ESRI.ArcGIS.Geometry.IPoint point, bool Clone, bool allowUndo)
    {
      base.AddPoint(point, Clone, allowUndo);
    }

    public override void Initialize(IEditor pEditor)
    {
      m_editor = pEditor;
      m_EdSketch = m_editor as IEditSketch3;
      base.Initialize(pEditor);
    }

    public override int Cursor
    {
      get
      {
        return base.Cursor;
      }
    }
    public override void Deactivate()
    {
      base.Deactivate();
    }
    public override double DistanceConstraint
    {
      get
      {
        return base.DistanceConstraint;
      }
      set
      {
        base.DistanceConstraint = value;
      }
    }
    public override bool Enabled
    {
      get
      {
        return base.Enabled;
      }
    }
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
    public override string ID
    {
      get
      {
        return base.ID;
      }
    }
    public override bool IsStreaming
    {
      get
      {
        return base.IsStreaming;
      }
      set
      {
        base.IsStreaming = value;
      }
    }
    public override ESRI.ArcGIS.Geometry.IPoint Location
    {
      get
      {
        return base.Location;
      }
    }
    public override bool OnContextMenu(int X, int Y)
    {
      return base.OnContextMenu(X, Y);
    }
    public override void OnKeyDown(int keyState, int shift)
    {
      base.OnKeyDown(keyState, shift);
    }
    public override void OnKeyUp(int keyState, int shift)
    {
      base.OnKeyUp(keyState, shift);
    }
    public override void OnMouseDown(int Button, int shift, int X, int Y)
    {
      base.OnMouseDown(Button, shift, X, Y);
    }
    public override void OnMouseMove(int Button, int shift, int X, int Y)
    {
      //Determine tangent to last segment on edit sketch
      //Set as angle constraint +90

      ISegmentCollection pSc = m_EdSketch.Geometry as ISegmentCollection;
      if (pSc.SegmentCount > 0)
      {
        ICurve3 pCurve;
        if (m_EdSketch.GeometryType == esriGeometryType.esriGeometryPolygon)
          pCurve = pSc.get_Segment(pSc.SegmentCount - 2) as ICurve3;
        else
          pCurve = pSc.get_Segment(pSc.SegmentCount - 1) as ICurve3;

        ILine pLine = new Line();
        pCurve.QueryTangent(esriSegmentExtension.esriExtendTangentAtTo, 1, true, 10, pLine);

        base.AngleConstraint = pLine.Angle + (90 * Math.PI / 180);
        base.Constraint = esriSketchConstraint.esriConstraintAngle;
      }
      base.OnMouseMove(Button, shift, X, Y);
    }
    public override void OnMouseUp(int Button, int shift, int X, int Y)
    {
      base.OnMouseUp(Button, shift, X, Y);
    }
    public override void Refresh(int hdc)
    {
      base.Refresh(hdc);
    }
    public override void SketchModified()
    {
      base.SketchModified();
    }
    public override string ToString()
    {
      return base.ToString();
    }

    #region IPersist Members

    public void GetClassID(out Guid pClassID)
    {
      //Explicitly set a guid so its different from the aggregated straight constructor
      //Used to set command.checked property
      pClassID = new Guid("8bfa7de0-8f39-11dd-ad8b-0800200c9a66");
    }

    #endregion

  }
}
