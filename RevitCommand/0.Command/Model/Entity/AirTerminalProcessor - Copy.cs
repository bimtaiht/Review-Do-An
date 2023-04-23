using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using SingleData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Utility;

namespace Model.Entity
{
    //class công cụ, chứa khối lệnh để xử lí đối tượng AirTerminalProcessor
    public static class AirTerminalProcessorUtil
    {
        //chỗ lưu trữ các dối tượng Revit
        private static RevitData revitData => RevitData.Instance;

        public static Duct GetTempDuct(this AirTerminalProcessor q)
        {
            var mainDuct = q.MainDuct!;
            var airTerminal = q.AirTerminal!;

            var doc = revitData.Document;

            var airTerminalConnector = airTerminal.MEPModel.ConnectorManager.UnusedConnectors.Cast<Connector>()
                .First(x => x.CoordinateSystem.BasisZ.IsPerpendicular(XYZ.BasisZ));

            var airTerminalConnectorDirection = airTerminalConnector.CoordinateSystem.BasisZ;
            var airTerminalConnectorOrigin = airTerminalConnector.Origin;

            var mainDuctLocationLine = ((mainDuct.Location as LocationCurve)!.Curve as Line)!;
            var projectionPoint = mainDuctLocationLine.GetProjectPoint(airTerminalConnectorOrigin);

            var endpoint = projectionPoint - airTerminalConnectorDirection * mainDuct.Width / 2;

            
                var systemTypeId = mainDuct.MEPSystem.GetTypeId();
                var levelId = mainDuct.LookupParameter("Reference Level").AsElementId();
                // create tempDuct
                var tempDuctTypeId = 142428.GetElementId();
                var tempDuctStartPoint = endpoint - airTerminalConnectorDirection * 100.0.milimeter2Feet();
                var tempDuctEndPoint = tempDuctStartPoint - airTerminalConnectorDirection * 400.0.milimeter2Feet();
                var tempDuct = Duct.Create(doc, systemTypeId, tempDuctTypeId, levelId, tempDuctStartPoint, tempDuctEndPoint);
                var tempDuctDiameter = airTerminalConnector.Radius * 2;
                tempDuct.LookupParameter("Diameter").Set(tempDuctDiameter);
                return tempDuct;
        }

       public static FamilyInstance GetTap( this AirTerminalProcessor q)
       {
            var x = 3;
            var x1 = 3;
            var x2 = 3;
            var x3 = 3;
            var x4 = 3;
            return q.AirTerminal!;
       }


        public static void Do(this AirTerminalProcessor q )
        {
            var doc = revitData.Document;
            using (var transaction = new Transaction(doc, "AirTerminal connnect"))
            {
                transaction.Start();
                //var tap = q.Tap;
                var tempDuct = q.TempDuct;
                transaction.Commit();

            }
        }

    }
}
