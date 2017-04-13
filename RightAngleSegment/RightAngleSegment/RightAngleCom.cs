using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Editor;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Drawing;
using System.Runtime.InteropServices;



namespace RightAngleSegment
{
  [Guid("e7b03140-8f37-11dd-ad8b-0800200c9a66")]
  [ClassInterface(ClassInterfaceType.None)]
  [ProgId("RightAngleSegment.RightAngleCom")]

  public sealed class RightAngleCom : BaseCommand
  {
    #region COM Registration Function(s)
    [ComRegisterFunction()]
    [ComVisible(false)]
    static void RegisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryRegistration(registerType);

      //
      // TODO: Add any COM registration code here
      //
    }

    [ComUnregisterFunction()]
    [ComVisible(false)]
    static void UnregisterFunction(Type registerType)
    {
      // Required for ArcGIS Component Category Registrar support
      ArcGISCategoryUnregistration(registerType);

      //
      // TODO: Add any COM unregistration code here
      //
    }

    #region ArcGIS Component Category Registrar generated code
    /// <summary>
    /// Required method for ArcGIS Component Category registration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryRegistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Register(regKey);

    }
    /// <summary>
    /// Required method for ArcGIS Component Category unregistration -
    /// Do not modify the contents of this method with the code editor.
    /// </summary>
    private static void ArcGISCategoryUnregistration(Type registerType)
    {
      string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
      MxCommands.Unregister(regKey);

    }

    #endregion
    #endregion

    private IEditor m_editor;
    private IEditSketch3 m_edSketch;
    private IApplication m_application;

    public RightAngleCom()
    {
      base.m_category = "Developer Samples"; //localizable text
      base.m_caption = "Right Angle Segment Constructor"; //localizable text
      base.m_message = "Creates right angle segments"; //localizable text 
      base.m_toolTip = "Right Angle Segment"; //localizable text 
      base.m_name = "DeveloperSamples_RightAngleSegment"; //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")
      base.m_bitmap = new Bitmap(GetType().Assembly.GetManifestResourceStream("RightAngleSegment.RightAngle.bmp"));
    }

    #region Overriden Class Methods
    public override void OnCreate(object hook)
    {
      if (hook == null)
        return;

      m_application = hook as IApplication;

      //Disable if it is not ArcMap
      if (hook is IMxApplication)
        base.m_enabled = true;
      else
        base.m_enabled = false;

      //get the editor
      UID editorUid = new UID();
      editorUid.Value = "esriEditor.Editor";
      m_editor = m_application.FindExtensionByCLSID(editorUid) as IEditor;
      m_edSketch = m_editor as IEditSketch3;
    }

    /// <summary>
    /// Occurs when this command is clicked
    /// </summary>
    public override void OnClick()
    {
      //m_edSketch = m_editor as IEditSketch3;
      RightAngleSC prasc = new RightAngleSC();
      prasc.Initialize(m_editor);
      m_edSketch.ShapeConstructor = prasc;
    }
    
    public override bool Enabled
    {
      // Enable the command if editing 
      // and current tool implements IShapeContructorTool
      get 
      { 
        bool pEnabled = false;
        if (m_editor.EditState == esriEditState.esriStateEditing)
        {
          //Check for IShapeConstructorTool
          IShapeConstructorTool psct = m_application.CurrentTool.Command as IShapeConstructorTool;
          pEnabled = (psct != null);
        }
        return (pEnabled); 
      }
    }

    public override bool Checked
    {
      get
      {
        // Check the command/button if we are the current constructor
        IPersist ptemp = m_edSketch.ShapeConstructor as IPersist;
        Guid pg;
        ptemp.GetClassID(out pg);
        return (pg.ToString() == "8bfa7de0-8f39-11dd-ad8b-0800200c9a66");
      }
    }

    #endregion

  }
}