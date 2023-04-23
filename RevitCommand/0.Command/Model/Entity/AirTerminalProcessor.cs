using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class AirTerminalProcessor
    {
        public Duct? MainDuct { get; set; }

        public FamilyInstance? AirTerminal { get; set; }

        private Duct tempDuct;

        public Duct TempDuct => this.tempDuct ??= this.GetTempDuct();

        private FamilyInstance? tap;  //chỗ lưu trữ giá trị
        public FamilyInstance? Tap     //phương thức truy xuất
            => this.tap ??=this.GetTap();


        //{
        //    get
        //    {
        //        //nếu chưa có giá trị thì tiếp phương thức xử lí 
        //        //nếu có giá trị thì bỏ qua phương thức và trả ra giá trị

        //        if(this.tap==null)
        //        {
        //            this.tap = this.GetTap();
        //                //AirTerminalProcessorUtil.GetTap(this);
        //        }
        //        return this.tap;
        //    }

        //}
     
                
        public FlexDuct? FlexDuct { get; set; }

    }
}
