using System;
using TomorrowSoft.BLL;
using TomorrowSoft.Model;
using FormUI.Properties;
namespace FormUI.Filters
{
    public class ConditionFilter
    {
         private static string _photovoltaic;
        private static string _battery;
        public static string content;
         public static Condition FilterCondition(string phone,string context)
         {
             string[] result = context.Split(new[] { "光伏", "\r\n", "\n", "\r\t","电池",
             "市电","功放","1喇叭","2喇叭","3喇叭","4喇叭"}, 
             StringSplitOptions.RemoveEmptyEntries);
             var condition = new Condition()
                 {
                     PhoneNo = phone,
                     Battery = result[0],
                     Photovoltaic = result[1],
 
                     Horn1 = result[2],
                     Horn2 = result[3],
                     Horn3 = result[4],
                     Horn4 = result[5],
                     HandlerTime = DateTime.Now.ToLocalTime()
                     
                 };
   
             _battery = result[0].Replace( "V",string.Empty);
             _photovoltaic = result[1].Replace("V", string.Empty);

             return condition;

         }
        public static bool PhotovoltaicCompare()
        {
            var qs = new QsService().GetAll();
            if (qs.Rows.Count>0)
            {
                if (double.Parse(_photovoltaic) < Convert.ToDouble(Settings.Default.RDS))
                {
                    content = string.Format("告警：光伏值：{0}低于{1}\r\n",_photovoltaic , Settings.Default.RDS);
                }
                if (double.Parse(_battery) < Convert.ToDouble(Settings.Default.Battery))
                {
                    content += string.Format("告警：电池值：{0}低于{1}\r\n",_battery , Settings.Default.Battery);
                }
                return ((double.Parse(_photovoltaic) < Convert.ToDouble(Settings.Default.RDS)) &&
                        double.Parse(_battery) < Convert.ToDouble(Settings.Default.Battery));

                
            }
            return false;
        }
    }
}