using System;
using System.Collections.Generic;


namespace PayrollReports.Models
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
        public static void GetWageComponent( WageModel model, Dictionary<string, string> MainData) {            
            
            if(GetComponentName(model.ElementCode) != string.Empty){
                MainData.Add(GetComponentName(model.ElementCode)+"Actual", model.ActualValue );
                MainData.Add(GetComponentName(model.ElementCode)+"Earned", model.EarnedValue );
            }
            
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

        public static string ConvertNumbertoWords(long number)   
        {  
            if (number == 0) return "ZERO";  
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));  
            string words = "";  
            if ((number / 1000000) > 0)   
            {  
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";  
                number %= 1000000;  
            }  
            if ((number / 1000) > 0)   
            {  
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";  
                number %= 1000;  
            }  
            if ((number / 100) > 0)   
            {  
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";  
                number %= 100;  
            }             
            if (number > 0)   
            {  
                if (words != "") words += "AND ";  
                var unitsMap = new[]   
                {  
                    "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"  
                };  
                var tensMap = new[]   
                {  
                    "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"  
                };  
                if (number < 20) words += unitsMap[number];  
                else   
                {  
                    words += tensMap[number / 10];  
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];  
                }  
            }  
            return words;  
        }  
    }


}