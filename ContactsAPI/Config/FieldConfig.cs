using ContactsAPI.Models;
using Newtonsoft.Json;

namespace ContactsAPI.Config
{
    public class FieldConfig
    {
        

        public static Dictionary<string, dynamic> UsingDynamic(string jsonString)
        {
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(jsonString)!;
            Dictionary<string, dynamic[]> configDict = new Dictionary<string, dynamic[]>();
            
            configDict.Add("EmployeeName", new dynamic[] { dynamicObject.Name, dynamicObject.Name1, dynamicObject.EmployeeName, dynamicObject.EmployeeName1 });
            configDict.Add("EmployeeEmail", new dynamic[] { dynamicObject.Email, dynamicObject.EmployeeEmail, dynamicObject.EmployeeEmail1 });
            configDict.Add("EmployeeAddress", new dynamic[] { dynamicObject.Address, dynamicObject.EmployeeAddress, dynamicObject.EmployeeAddress1 });
            configDict.Add("EmployeePhone", new dynamic[] { dynamicObject.Phone, dynamicObject.EmployeePhone });

            Dictionary<string, dynamic> mappedDict = new Dictionary<string, dynamic>();
            dynamic EmployeeName=""; dynamic EmployeeEmail=""; dynamic EmployeeAddress=""; dynamic EmployeePhone=0;
            foreach (dynamic field in configDict["EmployeeName"]) if (field != null) EmployeeName = field;
            foreach (dynamic field in configDict["EmployeeEmail"]) if (field != null) EmployeeEmail = field;
            foreach (dynamic field in configDict["EmployeeAddress"]) if (field != null) EmployeeAddress = field;
            foreach (dynamic field in configDict["EmployeePhone"]) if (field != null) EmployeePhone = field;

            mappedDict.Add("EmployeeName", EmployeeName);
            mappedDict.Add("EmployeeEmail", EmployeeEmail);
            mappedDict.Add("EmployeeAddress", EmployeeAddress);
            mappedDict.Add("EmployeePhone", EmployeePhone);

            return mappedDict;
        }

    }
}
