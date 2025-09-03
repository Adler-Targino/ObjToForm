using Microsoft.AspNetCore.Html;
using ObjToForm.Interfaces;
using ObjToForm.Services;

namespace ObjToForm
{
    public static class ObjToForm
    {
        public static IHtmlContent ConvertToRawHtmlForm(Type obj)
        {
            IObjectConvertService convertService = new ObjToRawHtml();
            return convertService.ConvertToForm(obj);
        }

        public static IHtmlContent ConvertToRawHtmlForm(object obj)
        {
            IObjectConvertService convertService = new ObjToRawHtml();
            return convertService.ConvertToForm(obj.GetType());
        }
    }
}
