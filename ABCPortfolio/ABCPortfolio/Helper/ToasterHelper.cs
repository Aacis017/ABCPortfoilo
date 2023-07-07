using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ABCPortfolio.Helper
{
    public static class ToasterHelper{ 
        public static void AddToastMessage(this ITempDataDictionary tempData, string title, string message, ToastType toastType = ToastType.Info)
            {
                tempData["ToastTitle"] = title;
                tempData["ToastMessage"] = message;
                tempData["ToastType"] = toastType.ToString().ToLower();
            }
        

        public enum ToastType
        {
            Success,
            Info,
            Warning,
            Error
        }
    }
}
