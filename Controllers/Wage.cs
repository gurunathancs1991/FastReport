
using System.Collections.Generic;


namespace SPAWebReport.Models
{

    public class WageModel {

        public string WageTo {get; set;}
        public string PayDate {get; set;}
        public string Remarks {get; set;}
        public string LOPValue {get; set;}
        public string WageFrom {get; set;}
        public string PaidValue {get; set;}
        public string ActualValue {get; set;}
        public string EarnedValue {get; set;}
        public string ElementCode {get; set;}
        public string EmployeeCode {get; set;}
        public string EmployeeName {get; set;}
        public string AdjustmentValue {get; set;}

    }

    public class WageProcess {
        public static Dictionary<string, string> GetWageComponent( WageModel model) {            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if(GetComponentName(model.ElementCode) != string.Empty){
                dic.Add(GetComponentName(model.ElementCode)+"Actual", model.ActualValue );
                dic.Add(GetComponentName(model.ElementCode)+"Earned", model.EarnedValue );
            }           
            return dic;
        }

        public static string GetComponentName (string key){
            string value = string.Empty;
            switch(key) {
                case "Char Comp" :
                    value = "HRA";
                    break;
                case "Vari Comp":
                    value = "Variable";
                    break;
                case "B4": 
                    value = "Special";
                    break;
                case "Deduct Comp": 
                    value = "ProfessionalTax";
                    break;
                case "BASIC":
                    value = "Basic";
                    break;
                case "GE":
                    value = "GE";
                    break;
                case "GD":
                    value = "GD";
                    break;
                case "NP":
                    value = "NP";
                    break;
            }
            return value;
        }
    }


}