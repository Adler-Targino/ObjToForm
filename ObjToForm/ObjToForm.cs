using Microsoft.AspNetCore.Html;
using ObjToForm.Interfaces;
using ObjToForm.Services;

namespace ObjToForm
{
    public static class ObjToForm
    {
        public static IHtmlContent ConvertToRawHtmlForm(object obj, string Prefix="", bool ModelBinding = false)
        {
            IObjectConvertService convertService = new ObjToRawHtml();
            return convertService.ConvertToForm(obj, Prefix, ModelBinding);
        }

        public static IHtmlContent ConvertToBootstrapForm(object obj, string Prefix = "", bool ModelBinding = false)
        {
            IObjectConvertService convertService = new ObjToBootstrap();
            return convertService.ConvertToForm(obj, Prefix, ModelBinding);
        }
    }
}
