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

        public static IHtmlContent ConvertToBootstrapForm(Type obj)
        {
            IObjectConvertService convertService = new ObjToBootstrap();
            return convertService.ConvertToForm(obj);
        }

        public static IHtmlContent ConvertToBootstrapForm(object obj)
        {
            IObjectConvertService convertService = new ObjToBootstrap();
            return convertService.ConvertToForm(obj.GetType());
        }
    }
}
