#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

#endregion

namespace TagAll
{
    [Transaction(TransactionMode.Manual)]
    public class cmdTagAll : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document curDoc = uidoc.Document;

            #region Code for Form

            // get all annotation floor plan views
            List<View> floorPlans = Utils.GetAllViewsByNameContains(curDoc, "Annotation");

            // get all dimension floor plan views
            List<View> dimensionPlans = Utils.GetAllViewsByNameContains(curDoc, "Dimension");

            // get all section views
            List<View> sectionViews = Utils.GetAllViewsByNameContains(curDoc, "Front/Rear", "Left/Right");

            // get all door tag types
            FilteredElementCollector colDrTags = new FilteredElementCollector(curDoc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_DoorTags)
                .WhereElementIsElementType();

            // get all window tag types
            FilteredElementCollector colWndwTags = new FilteredElementCollector(curDoc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_WindowTags)
                .WhereElementIsElementType();

            // get all room tag types
            FilteredElementCollector colRoomTags = new FilteredElementCollector(curDoc)
                .OfClass(typeof(FamilySymbol))
                .OfCategory(BuiltInCategory.OST_RoomTags)
                .WhereElementIsElementType();

            #endregion

            // open form
            frmTagAll curForm = new frmTagAll()
            {
                Width = 350,
                Height = 320,
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen,
                Topmost = true,
            };

            curForm.ShowDialog();

            // get form data and do something

            // start a transaction group

            // check if the tag doors box is checked
            if (curForm.GetCheckBoxDoors() == true || curForm.GetCheckBoxWndws() == true)
            {
                int countFloor = floorPlans.Count;

                while (countFloor > 0)
                {
                    // loop through the views & set each one as the active view

                    // start a transaction to tag all doors & windows in the active view

                    // if double tag is checked add tag, then add mark

                    // commit the tansaction

                    countFloor--;
                }
            }
            
            // check if the tag all openings box is checked
            if (curForm.GetCheckBoxOpenings() == true )
            {
                int countDimension = dimensionPlans.Count;

                while (countDimension > 0)
                {
                    // loop through the views & set each one as the active view

                    // start a transaction to tag all openings in the active view

                    // commit the tansaction

                    countDimension--;
                }
            }

            // check if the tag all rooms box is checked
            if (curForm.GetCheckBoxRooms() == true )
            {
                int countSections = sectionViews.Count;

                while (countSections > 0)
                {
                    // loop through the views & set each one as the active view

                    // start a transaction to tag all rooms in the active view

                    // commit the tansaction

                    countSections--;
                }
            } 

            return Result.Succeeded;
        }

        public static String GetMethod()
        {
            var method = MethodBase.GetCurrentMethod().DeclaringType?.FullName;
            return method;
        }
    }
}
